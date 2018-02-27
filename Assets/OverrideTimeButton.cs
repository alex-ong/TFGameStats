using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverrideTimeButton : MonoBehaviour {
    public InputField inputField;
    public BarchartMaster barchartMaster;
	public void OnClick()
    {
        int value = 30;
        if (inputField.text.Contains(":"))
        {
            try
            {
                string[] splits = inputField.text.Split(new char[] { ':' });
                value = int.Parse(splits[0]) * 60 + int.Parse(splits[1]);
            } catch {
                value = 30;
            }
        } else
        {
            int.TryParse(inputField.text, out value);
        }
        
        barchartMaster.UpdateTimeOnly(value);
    }
}
