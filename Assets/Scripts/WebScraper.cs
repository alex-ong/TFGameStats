using System.Collections;
using System.Collections.Generic;
using System.Net;
using System;
using System.Threading;

using UnityEngine;

public class WebScraper : MonoBehaviour
{
    public Thread t;

    [SerializeField]
    private string playerName = "XeaL";
    [SerializeField]
    private string id = "62080";
    protected object playerIDLock = new object();
    private bool stillRunning = true;

    protected object TransactionTableLock = new object();
    protected volatile TransactionTable latestEvent = null;

    //returns latestEvent if set. Sets it to null as soon as you grab it.
    public TransactionTable LatestEvent
    {
        get
        {
            TransactionTable result = null;
            lock (TransactionTableLock)
            {
                result = latestEvent;
                latestEvent = null;
            }
            return result;
        }
    }

    public void OnApplicationQuit()
    {
        stillRunning = false;
    }

    public void SetPlayerNameAndID(string playerName, string id)
    {
        lock (playerIDLock)
        {
            this.playerName = playerName;
            this.id = id;
        }
    }

    public const string mpStats = "http://www.tetrisfriends.com/leaderboard/ajax/mp_user_stats.php?username={0}&productId=3&userId={1}";

    void Start()
    {
        Thread t = new Thread(RunScraper);
        t.Start();
    }

    private string[] Scrape()
    {
        WebClient client = new WebClient();
        string name;
        string identity;

        lock (playerIDLock)
        {
            name = playerName;
            identity = id;
        }

        string htmlCode = client.DownloadString(string.Format(mpStats, name, identity));
        string[] lines = htmlCode.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        return lines;
    }

    void RunScraper()
    {
        TransactionTable data = new TransactionTable();
        while (stillRunning)
        {
            string[] lines = Scrape();

            foreach (string line in lines)
            {
                string line2 = line.Trim();
                if (line2.StartsWith("<div class = 'user_stats_table_box'>"))
                {
                    if (ParseLine(line2, data)) //new result!
                    {
                        lock (TransactionTableLock)
                        {
                            latestEvent = new TransactionTable(data); //make a copy of the data.                            
                        }
                    }
                }
            }
        }
    }

    string previousLine = null;

    protected bool ParseLine(string s, TransactionTable data)
    {
        if (s != previousLine)
        {
            previousLine = s;
            //pull out all transactions...
            int startTransaction = 0;
            int endTransaction;
            List<string> transactionStrings = new List<string>();

            startTransaction = s.IndexOf("<tr");
            while (startTransaction != -1)
            {
                endTransaction = s.IndexOf("/tr>", startTransaction) + "/tr>".Length;
                string transaction = s.Substring(startTransaction,
                                                 endTransaction - startTransaction);
                transactionStrings.Add(transaction);
                startTransaction = s.IndexOf("<tr", endTransaction);
            }

            List<Transaction> transactions = new List<Transaction>();
            foreach (string transactionString in transactionStrings)
            {
                Transaction t = Transaction.ProcessTransaction(transactionString);
                if (t != null)
                {
                    transactions.Add(t);
                }
            }

            data.UpdateTransactionTable(transactions);
            return true;
        }
        return false;
    }

}
