using UnityEngine;
using System.Collections;

/// <summary>
/// 角色按鈕屬性
/// </summary>
public class RoleButton : MonoBehaviour
{
    public GameDefinition.SystemPlayerName SystemName;

    public float AmplifyScale;  //放大倍率
    private float originScale;  //原始倍率

    private bool isChoosed = false; //是否被選中

    // Use this for initialization
    void Start()
    {
        this.originScale = this.transform.localScale.x;     //紀錄原始scale大小

        this.GetComponentInChildren<TextMesh>().text = GameDefinition.PlayerNameData[this.SystemName];  //抓取系統儲存的玩家名字
    }

    void OnMouseEnter()
    {
        if (!this.isChoosed)
        {
            //滑鼠移進角色按鈕，按鈕放大(AmplifyScale 決定)
            iTween.ScaleTo(this.gameObject, iTween.Hash(
                    "scale", Vector3.one * this.AmplifyScale,
                    "time", 1
                    ));
        }
    }

    void OnMouseExit()
    {
        if (!this.isChoosed)
        {
            //滑鼠移出角色按鈕，按鈕回到原始大小
            iTween.ScaleTo(this.gameObject, iTween.Hash(
                    "scale", Vector3.one * this.originScale,
                    "time", 1
                    ));
        }
    }

    void OnMouseUpAsButton()
    {
        if (!this.isChoosed)
        {
            foreach (RoleButton tempScript in this.transform.parent.GetComponentsInChildren<RoleButton>())
            {
                //將沒被選中的角色按鈕關閉 
                if (tempScript != this)
                    tempScript.gameObject.SetActive(false);
            }

            //紀錄目前被選擇的腳色
            GameDefinition.CurrentChoosePlayerName = this.SystemName;

            // ITween 動畫， 將被選中的按鈕移動至 預先設定的位置與大小(RoleButtonController scipt 的 ChoosePropertiesObject)
            iTween.ScaleTo(this.gameObject, iTween.Hash(
                    "scale", RoleButtonManager.script.ChoosePropertiesObject.transform.localScale,
                    "time", 1
                    ));
            iTween.MoveTo(this.gameObject, iTween.Hash(
                    "position", RoleButtonManager.script.ChoosePropertiesObject.transform.position,
                    "time", 1,
                    "oncomplete", "ChooseMoveComplete"      //動畫完成後 callback
                    ));

            this.isChoosed = true;  //設定為已被選中
        }
    }

    /// <summary>
    /// ITween 動畫完成 callback (移動事件)
    /// </summary>
    void ChooseMoveComplete()
    {
        EventCollection.script.NextEvent();     //切換至下一事件

        //切換事件同時，將按鈕顏色淡出
        iTween.ValueTo(this.gameObject, iTween.Hash(
                "from", 1,
                "to", 0,
                "time", 0.75f,
                "onupdate", "RoleButtonDisappear"   //動畫進行 callback
                ));
    }

    /// <summary>
    /// ITween 動畫 Update (顏色消失事件)
    /// </summary>
    /// <param name="a"></param>
    void RoleButtonDisappear(float a)
    {
        //將RoleButton按鈕消失，包含(1)Sprite按鈕圖、(2)TextMesh名字
        SpriteRenderer sr = this.GetComponent<SpriteRenderer>();
        TextMesh tm = this.GetComponentInChildren<TextMesh>();

        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, a);
        tm.color = new Color(tm.color.r, tm.color.g, tm.color.b, a);
    }
}