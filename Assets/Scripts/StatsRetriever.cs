using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsRetriever : MonoBehaviour
{
    public WebScraper p1;
    public WebScraper p2;

    TransactionTable t1;
    TransactionTable t2;

    public BarchartMaster barchartMaster;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (t1 == null)
        {
            t1 = p1.LatestEvent;                        
        }
        if (t2 == null)
        {
            t2 = p2.LatestEvent;
        }

        if (t1 != null && t2 != null)
        {
            UpdateStats();
            t1 = null;
            t2 = null;
        }

    }

    public void SetNamesAndIDs(string p1name, int p1ID, string p2name, int p2ID)
    {
        p1.SetPlayerNameAndID(p1name, p1ID.ToString());
        p2.SetPlayerNameAndID(p1name, p2ID.ToString());
    }

    public void UpdateStats()
    {
        barchartMaster.UpdatePair(t1.toRawBarChartInfo(), t2.toRawBarChartInfo());
    }
}
