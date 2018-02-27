using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoadPlayerButtonLogic : MonoBehaviour
{
    public InputField p1name;
    public InputField p1ID;
    public InputField p2name;
    public InputField p2ID;
    public Text p1label;
    public Text p2label;
    public StatsRetriever statsRetriever;

    public void OnButtonPress()
    {
        string name1 = p1name.text;
        int p1 = 0; int.TryParse(p1ID.text, out p1);
        string name2 = p2name.text;
        int p2 = 0; int.TryParse(p2ID.text, out p2);
        statsRetriever.SetNamesAndIDs(name1, p1, name2, p2);
        p1label.text = name1;
        p2label.text = name2;
    }
}
