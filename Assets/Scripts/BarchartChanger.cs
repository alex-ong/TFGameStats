using UnityEngine;
using UnityEngine.UI;

public class BarchartChanger : MonoBehaviour {
    public RectTransform bar;
    public Text t;
    public float maxHeight = 100f;
    public float currentValue = 0.0f;

    public void SetNumber(int number, float perc)
    {
        Rect r = bar.rect;
        r.height = perc * maxHeight;
        t.text = number.ToString();
        currentValue = number;
    }

    public void SetNumber(float number, float perc)
    {
        Rect r = bar.rect;
        r.height = perc * maxHeight;
        t.text = number.ToString("0.00");
        currentValue = number;
    }

}
