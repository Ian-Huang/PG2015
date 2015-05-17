using UnityEngine;
using System.Collections;

public class GameTimer : MonoBehaviour
{
    public TextMesh MinuteText;
    public TextMesh SecondText;

    public TimerEndtoDo EndtoDo;
    public bool isStartRun = false;

    public int CountDownSecond;
    public int WarningTime = 10;
    public Color WarningColor = new Color(1, 0, 0, 1);

    // Use this for initialization
    IEnumerator Start()
    {
        this.MinuteText.text = "";
        this.SecondText.text = "";
        yield return new WaitForSeconds(this.GetComponent<ColorTo>().DelayTime + this.GetComponent<ColorTo>().ChangeTime);

        //如果是區域地圖計時器，讀取系統紀錄遊戲時間
        if (this.EndtoDo == TimerEndtoDo.AreaChangeTimeOutHint)
            this.CountDownSecond = GameDefinition.CurrentGameTime;

        if (this.CountDownSecond <= this.WarningTime)
        {
            this.MinuteText.color = this.WarningColor;
            this.SecondText.color = this.WarningColor;
        }

        this.MinuteText.text = (this.CountDownSecond / 60).ToString("00");
        this.SecondText.text = (this.CountDownSecond % 60).ToString("00");

        if (this.isStartRun)
            InvokeRepeating("RunTimer", 1, 1);
    }

    public void StartTimer()
    {
        InvokeRepeating("RunTimer", 1, 1);
    }

    void RunTimer()
    {
        this.CountDownSecond--;

        if (this.CountDownSecond < 0)
        {
            switch (this.EndtoDo)
            {
                case TimerEndtoDo.NextGameStep:
                    if (this.gameObject.activeInHierarchy)
                        GameCollection.script.NextGameStep();
                    break;
                case TimerEndtoDo.AreaChangeTimeOutHint:
                    GameDefinition.CurrentGameTime = 0;
                    break;
                default:
                    break;
            }

            CancelInvoke("RunTimer");
        }
        else
        {
            if (this.CountDownSecond == this.WarningTime)
            {
                this.MinuteText.color = this.WarningColor;
                this.SecondText.color = this.WarningColor;
            }
            this.MinuteText.text = (this.CountDownSecond / 60).ToString("00");
            this.SecondText.text = (this.CountDownSecond % 60).ToString("00");
        }
    }

    public enum TimerEndtoDo
    {
        Nothing = 0, NextGameStep = 1, AreaChangeTimeOutHint = 2
    }
}
