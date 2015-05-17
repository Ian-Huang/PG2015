using UnityEngine;
using System.Collections;

/// <summary>
/// 通用函式，Sprite Color 、 TextMesh Color 漸變控制
/// </summary>
public class ColorTo : MonoBehaviour
{
    public Color StartColor = new Color(1, 1, 1, 0);    //起始顏色
    public Color EndColor = new Color(1, 1, 1, 1);      //結束顏色
    public float ChangeTime;                            //過程花費時間
    public float DelayTime;                             //延遲
    public bool isStartChange;
    public bool isAutoBack = false;
    public float AutoBackDelayTime;
    public iTween.EaseType easeType;
    public iTween.LoopType loopType = iTween.LoopType.none;

    private SpriteRenderer spriteRenderer;
    private TextMesh textMesh;

    void Awake()
    {
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
        this.textMesh = this.GetComponent<TextMesh>();
    }

    void OnEnable()
    {
        //是否在物件被啟用時啟動函式
        if (this.isStartChange)
            this.StartColorTo();
    }

    public void StartColorTo()
    {
        if (this.spriteRenderer != null)
            this.spriteRenderer.color = this.StartColor;
        if (this.textMesh != null)
            this.textMesh.color = this.StartColor;

        iTween.ValueTo(this.gameObject, iTween.Hash(
                "from", this.StartColor,
                "to", this.EndColor,
                "time", this.ChangeTime,
                "delay", this.DelayTime,
                "onupdate", "ColorUpdate",
                "oncomplete", "ColorComplete",
                "easetype", this.easeType,
                "looptype", this.loopType));
    }

    void ColorUpdate(Color color)
    {
        if (this.spriteRenderer != null)
            this.spriteRenderer.color = color;
        if (this.textMesh != null)
            this.textMesh.color = color;
    }

    void ColorComplete()
    {
        if (this.isAutoBack)
        {
            iTween.ValueTo(this.gameObject, iTween.Hash(
                "from", this.EndColor,
                "to", this.StartColor,
                "time", this.ChangeTime,
                "delay", this.AutoBackDelayTime,
                "onupdate", "ColorUpdate",
                "oncomplete", "ColorComplete",
                "easetype", this.easeType,
                "looptype", this.loopType));
            this.isAutoBack = false;
        }
    }
}
