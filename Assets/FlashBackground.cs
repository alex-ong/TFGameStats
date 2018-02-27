using UnityEngine.UI;
using UnityEngine;
public class FlashBackground : MonoBehaviour
{
    public Image image;
    public float secondsPerFlash = 0.5f;

    [SerializeField]
    private bool flashing = false;

    private bool blue = true;
    private float timer;


    private void swap()
    {
        blue = !blue;
        if (blue)
        {
            image.color = Color.blue;
        }
        else
        {
            image.color = Color.red;
        }
    }

    public void flash()
    {
        flashing = true;
        timer = secondsPerFlash;
    }

    public void stop()
    {
        flashing = false;
        blue = true;
        image.color = Color.blue;
    }

    public void Update()
    {
        if (flashing)
        {
            timer += Time.deltaTime;
            if (timer >= secondsPerFlash)
            {
                timer = 0.0f;
                swap();
            }
        }

    }
}
