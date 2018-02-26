using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsRetriever : MonoBehaviour
{
    public WebScraper p1;
    public WebScraper p2;

    TransactionTable t1;
    TransactionTable t2;
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
            if (t1 != null)
            {
                Debug.Log(t1);
                t1 = null;
            }
        }
        //if (t2 == null)
        //{
        //    t2 = p2.LatestEvent;
        //}

        //if (t1 != null && t2 != null)
        //{
        //    UpdateStats();
        //    t1 = null;
        //    t2 = null;
        //}
    }

    public void UpdateStats()
    {
        
    }
}
