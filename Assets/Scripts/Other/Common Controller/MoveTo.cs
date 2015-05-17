using UnityEngine;
using System.Collections;

/// <summary>
/// 通用函式，物件Transforn Position 控制
/// </summary>
public class MoveTo : MonoBehaviour
{
    public Vector3 StartPoint;      //起始位置
    public Vector3 EndPoint;        //結束位置
    public float MoveTime;          //移動時間
    public float DelayTime;         //延遲
    public bool isStartMove;
    public iTween.EaseType easeType;
    public iTween.LoopType loopType = iTween.LoopType.none;

    void OnEnable()
    {
        if (this.isStartMove)
            this.Move();
    }

    public void Move()
    {
        this.transform.position = this.StartPoint;
        iTween.MoveTo(this.gameObject, iTween.Hash(
            "position", this.EndPoint,
            "time", this.MoveTime,
            "delay", this.DelayTime,
            "easetype", this.easeType,
            "looptype", this.loopType
            ));
    }
}