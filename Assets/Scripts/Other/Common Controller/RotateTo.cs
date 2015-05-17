using UnityEngine;
using System.Collections;

public class RotateTo : MonoBehaviour
{
    public Vector3 StartAngle;      //起始位置
    public Vector3 EndAngle;        //結束位置
    public float AngleTime;          //移動時間
    public float DelayTime;         //延遲
    public bool isStartRotate;
    public iTween.EaseType easeType;
    public iTween.LoopType loopType = iTween.LoopType.none;

    void OnEnable()
    {
        if (this.isStartRotate)
            this.RotateAngle();
    }

    public void RotateAngle()
    {
        this.transform.eulerAngles = this.StartAngle;
        iTween.RotateTo(this.gameObject, iTween.Hash(
            "rotation", this.EndAngle,
            "time", this.AngleTime,
            "delay", this.DelayTime,
            "easetype", this.easeType,
            "looptype", this.loopType
            ));
    }
}
