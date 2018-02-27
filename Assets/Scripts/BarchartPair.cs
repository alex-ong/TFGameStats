using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarchartPair : MonoBehaviour
{
    public BarchartChanger left;
    public BarchartChanger right;

    public float maxValue; //if they go over, its proportional.

    public void SetValues(int left, int right)
    {
        int max = Mathf.Max(left, right);
        if (max < maxValue)
        {
            this.left.SetNumber(left, left / maxValue);
            this.right.SetNumber(right, right / maxValue);
        }
        else
        {
            if (max == left)
            {
                this.left.SetNumber(left, 1.0f);
                this.right.SetNumber(right, right / 1.0f);
            }
            else
            {
                this.left.SetNumber(left, 1.0f);
                this.right.SetNumber(right, right / 1.0f);
            }
        } 
    }

    public void SetValues(float left, float right)
    {
        float max = Mathf.Max(left, right);
        if (max < maxValue)
        {
            this.left.SetNumber(left, left / maxValue);
            this.right.SetNumber(right, right / maxValue);
        }
        else
        {
            if (max == left)
            {
                this.left.SetNumber(left, 1.0f);
                this.right.SetNumber(right, right / 1.0f);
            }
            else
            {
                this.left.SetNumber(left, 1.0f);
                this.right.SetNumber(right, right / 1.0f);
            }
        }
    }

}
