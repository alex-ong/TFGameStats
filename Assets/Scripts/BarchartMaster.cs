using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarchartMaster : MonoBehaviour
{
    public BarchartPair Tetris;
    public BarchartPair TSpin;
    public BarchartPair BackToBack;
    public BarchartPair PPM;
    public BarchartPair APM;
    public BarchartPair PerfectClear;

    public Text leftPlayerName;
    public Text rightPlayerName;

    private RawBarchartInfo left;
    private RawBarchartInfo right;

    public FlashBackground flashBackground;
   
    public void UpdatePlayerNames(string left, string right)
    {
        leftPlayerName.text = left;
        rightPlayerName.text = right;
    }

    public class RawBarchartInfo
    {
        public int tetris;
        public int tspin;
        public int backtoback;
        public int piecesPlaced;
        public int linesSent;
        public int perfectClear;
        public int gameSeconds;


        public RawBarchartInfo(int tetris, int tspin, int backtoback, int piecesPlaced, 
                                int linesSent, int perfectClear, int gameSeconds)
        {
            this.tetris = tetris;
            this.tspin = tspin;
            this.backtoback = backtoback;
            this.piecesPlaced = piecesPlaced;
            this.linesSent = linesSent;
            this.perfectClear = perfectClear;
            this.gameSeconds = gameSeconds;
        }
    }

    public void UpdatePair(RawBarchartInfo left, RawBarchartInfo right)
    {
        this.left = left;
        this.right = right;
        this.processBarChartPairs(false);
    }

    public void UpdateTimeOnly(int seconds)
    {
        left.gameSeconds = seconds;
        right.gameSeconds = seconds;
        processBarChartPairs(true);
    }

    protected void processBarChartPairs(bool timeOnly = false)
    {
        if (!timeOnly) //update non timeonly ones
        {
            Tetris.SetValues(left.tetris, right.tetris);
            TSpin.SetValues(left.tspin, right.tspin);
            BackToBack.SetValues(left.backtoback, right.backtoback);
            if (left.gameSeconds > 0 && right.gameSeconds > 0)
            {
                PPM.SetValues(Mathf.RoundToInt((float)left.piecesPlaced / left.gameSeconds * 60), 
                              Mathf.RoundToInt((float)right.piecesPlaced / right.gameSeconds * 60));
                APM.SetValues(Mathf.RoundToInt((float)left.linesSent / left.gameSeconds * 60),
                              Mathf.RoundToInt((float)right.linesSent / right.gameSeconds * 60));
                flashBackground.stop();
            } else
            {
                flashBackground.flash();
            }
            PerfectClear.SetValues(left.perfectClear, right.perfectClear);            
        } else
        {
            if (left.gameSeconds > 0 && right.gameSeconds > 0)
            {
                PPM.SetValues(Mathf.RoundToInt((float)left.piecesPlaced / left.gameSeconds * 60),
                              Mathf.RoundToInt((float)right.piecesPlaced / right.gameSeconds * 60));
                APM.SetValues(Mathf.RoundToInt((float)left.linesSent / left.gameSeconds * 60),
                              Mathf.RoundToInt((float)right.linesSent / right.gameSeconds * 60));
                flashBackground.stop();
            } else
            {
                flashBackground.flash();
            }
        }
    }
}
