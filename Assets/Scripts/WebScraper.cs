using System.Collections;
using System.Collections.Generic;
using System.Net;
using System;
using System.Threading;

using UnityEngine;

public class WebScraper : MonoBehaviour
{
    public Thread t;

    private string playerName = "XeaL";
    private string id = "1840665";
    protected object playerIDLock = new object();
    private bool stillRunning = true;

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

        string htmlCode = client.DownloadString(string.Format(mpStats, playerName, identity));
        string[] lines = htmlCode.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        return lines;
    }

    void RunScraper()
    {
        while (stillRunning)
        {
            string[] lines = Scrape();

            foreach (string line in lines)
            {
                string line2 = line.Trim();
                if (line2.StartsWith("<div class = 'user_stats_table_box'>"))
                {
                    ParseLine(line2);
                }
            }
        }
    }

    string previousLine = null;
    
    void ParseLine(string s)
    {
        if (s != previousLine)
        {
            previousLine = s;
            Debug.Log(System.DateTime.Now);
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
                Transaction t = ProcessTransaction(transactionString);
                if (t != null)
                {
                    transactions.Add(t);
                }
            }

            foreach (Transaction t in transactions)
            {
                Debug.Log(t);
            }

        }
    }

    public class Transaction
    {
        string name;
        string value;
        public Transaction(string name, string value)
        {
            this.name = name;
            this.value = value;
        }

        public override string ToString()
        {
            return name + ":" + value;
        }
    }

    public class TransactionTable
    {
        public TransactionTable(List<string> originalData)
        {

        }
    }

    Transaction ProcessTransaction(string s)
    {
        int startName = s.IndexOf("<th>") + "<th>".Length;
        int endName = s.IndexOf("</th>", startName);
        string name = s.Substring(startName, endName - startName);

        int startValue = s.IndexOf("<td>") + "<td>".Length;
        int endValue = s.IndexOf("</td>", startValue);
        string value = s.Substring(startValue, endValue - startValue);

        if (name.Length > 0 && value.Length > 0)
        {
            return new Transaction(name, value);
        } else
        {
            return null;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
