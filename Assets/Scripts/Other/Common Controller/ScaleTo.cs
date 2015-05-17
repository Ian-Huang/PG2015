using UnityEngine;
using System.Collections;

public class ScaleTo : MonoBehaviour
{
    public Vector3 StartScale;      //起始位置
    public Vector3 EndScale;        //結束位置
    public float ScaleTime;          //移動時間
    public float DelayTime;         //延遲
    public bool isStartScale;
    public iTween.EaseType easeType;
    public iTween.LoopType loopType = iTween.LoopType.none;

    void OnEnable()
    {
        if (this.isStartScale)
            this.StartScaleTo();
    }

    public void StartScaleTo()
    {
        iTween.ScaleTo(this.gameObject, iTween.Hash(
            "scale", this.EndScale,
            "time", this.ScaleTime,
            "delay", this.DelayTime,
            "easetype", this.easeType,
            "looptype", this.loopType
            ));
    }
}
