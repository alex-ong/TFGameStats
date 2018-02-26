using UnityEngine;
using System.Collections.Generic;
public class Transaction
{
    public string name;
    public string value;
    public Transaction(string name, string value)
    {
        this.name = name;
        this.value = value;
    }

    public override string ToString()
    {
        return name + ":" + value;
    }

    public static Transaction ProcessTransaction(string s)
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
        }
        else
        {
            return null;
        }
    }
}


public class TransactionTable
{
    Dictionary<string, string> baseValues = new Dictionary<string, string>();
    Dictionary<string, int> diffValues = new Dictionary<string, int>();

    public int GamesPlayed { get { return diffValues["Games Played"]; } }
    public int timeSeconds { get { return diffValues["TimeDiffSeconds"]; } } //        Total Time
    public int LinesSent { get { return diffValues["Total Lines Sent"]; } }
    public int TotalTetriminos { get { return diffValues["Total Tetriminos"]; } }
    public int Singles { get { return diffValues["Total Singles"]; } }
    public int Doubles { get { return diffValues["Total Doubles"]; } }
    public int Triples { get { return diffValues["Total Triples"]; } }
    public int Tetrises { get { return diffValues["Total Tetrises"]; } }
    public int TSpins { get { return diffValues["Total T-Spins(All T-Spins)"]; } }
    public int BackToBacks { get { return diffValues["Total Back-to-Backs"]; } }
    public int PerfectClears { get { return diffValues["Perfect Clears"]; } }

    bool init = false;

    protected void Init(List<Transaction> newData)
    {
        init = true;
        foreach (Transaction t in newData)
        {
            baseValues[t.name] = t.value;
            diffValues[t.name] = 0;
        }
    }

    public void UpdateTransactionTable(List<Transaction> newData)
    {
        if (init)
        {
            foreach (Transaction t in newData)
            {          
                if (t.name == "Total Time") //special case in format hhhh:mm:ss
                {
                    diffValues[t.name] = parseTime(t.value) - parseTime(baseValues[t.name]);
                    baseValues[t.name] = t.value;
                }
                else
                {
                    int result;
                    if (int.TryParse(t.value, out result)) //skips floating point such as average tpm
                    {
                        diffValues[t.name] = int.Parse(t.value) - int.Parse(baseValues[t.name]);
                        baseValues[t.name] = t.value;
                    }
                    
                }
            }
        }
        else
        {
            Init(newData);
        }
    }

    protected int parseTime(string time)
    {
        string[] segments = time.Split(new char[] { ':' });
        //should always be three segments...
        return (int.Parse(segments[0]) * 60 * 60 + //hours
                int.Parse(segments[1]) + //minutes
                int.Parse(segments[2]));
    }

    public TransactionTable()
    {

    }

    public TransactionTable(TransactionTable other)
    {
        //no deepcopy required since strings are references in c#, and
        //ints are basic types.
        this.baseValues = new Dictionary<string, string>(other.baseValues);
        this.diffValues = new Dictionary<string, int>(other.diffValues);
    }

    public override string ToString()
    {
        string result = "";
        foreach (string key in diffValues.Keys)
        {
            result += key + ":" + diffValues[key].ToString() + "\n";
        }
        return result;
    }

}

