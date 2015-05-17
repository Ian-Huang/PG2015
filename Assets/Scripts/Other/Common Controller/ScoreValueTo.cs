using UnityEngine;
using System.Collections;

public class ScoreValueTo : MonoBehaviour
{
    public int StartValue;      //起始位置
    public int EndValue;        //結束位置
    public float RunTime;           //移動時間
    public float DelayTime;         //延遲
    public bool isStartRun;
    public iTween.EaseType easeType;

    private TextMesh textMesh;

    void Awake()
    {
        this.textMesh = this.GetComponent<TextMesh>();
    }

    void OnEnable()
    {
        if (this.isStartRun)
            this.StartRun();
    }

    public void StartRun()
    {
        iTween.ValueTo(this.gameObject, iTween.Hash(
            "from", this.StartValue,
            "to", this.EndValue,
            "time", this.RunTime,
            "delay", this.DelayTime,
            "onupdate", "ValueUpdate",
            "easetype", this.easeType
            ));
    }

    void ValueUpdate(int value)
    {
        this.textMesh.text = value.ToString();
    }
}
