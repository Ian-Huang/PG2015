using UnityEngine;
using System.Collections;

public class ReasoningGame_Card : MonoBehaviour
{
    public Sprite Card_Front;
    public Sprite Card_Back;

    public ReasoningGame_Manager.CardType cardType;

    public float RotateTime;        //轉動時間
    public iTween.EaseType easeType;


    // Use this for initialization
    void Start()
    {
        this.GetComponent<SpriteRenderer>().sprite = this.Card_Back;
        this.transform.eulerAngles = new Vector3(0, 180, 0);
    }

    #region Rotate Card

    /// <summary>
    /// 轉動卡片
    /// </summary>
    /// <param name="face">要轉動的方向</param>
    public void RotateCard()
    {
        iTween.RotateTo(this.gameObject, iTween.Hash("y", 90, "time", this.RotateTime, "easetype", this.easeType, "oncomplete", "RotateRun"));
        ReasoningGame_Manager.script.isRotating = true;
    }

    /// <summary>
    /// 轉動中繼點，更換圖片
    /// </summary>
    /// <param name="face"></param>
    void RotateRun()
    {
        this.GetComponent<SpriteRenderer>().sprite = this.Card_Front;

        iTween.RotateTo(this.gameObject, iTween.Hash("y", 0, "time", this.RotateTime, "easetype", this.easeType, "oncomplete", "RotateComplete"));
    }

    /// <summary>
    /// 轉動完成點
    /// </summary>
    void RotateComplete()
    {
        ReasoningGame_Manager.script.isRotating = false;    //修正轉動狀態
    }

    #endregion


}
