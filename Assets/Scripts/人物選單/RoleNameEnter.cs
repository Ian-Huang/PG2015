using UnityEngine;
using System.Collections;

/// <summary>
/// 輸入腳色名字的UI控制
/// </summary>
public class RoleNameEnter : MonoBehaviour
{
    public GameObject HintObject;   //提示物件
    public GameObject SureObject;   //確認按鈕
    public Rect UIRect;
    [HideInInspector]
    public string EnterNameString;  //輸入名字字串

    public GUIStyle style;
    private int originFontSize;

    void Start()
    {
        //名字輸入框文字與腳色姓名確認
        this.EnterStringCheck();

        //紀錄原始文字大小(如解析度發生改變進行倍率切換)
        this.originFontSize = this.style.fontSize;
    }


    void OnGUI()
    {
        this.style.fontSize = Mathf.FloorToInt(this.originFontSize * ((float)Screen.width / 640.0f));


        this.EnterNameString = GUI.TextField(
            new Rect(this.UIRect.x * Screen.width, this.UIRect.y * Screen.height, this.UIRect.width * Screen.width, this.UIRect.height * Screen.height),
            this.EnterNameString, 5, this.style).Replace("\n", "");

        //假如輸入框輸入內容產生改變
        if (GUI.changed)
        {
            //名字長度 =0，顯示輸入提示，確認按鈕關閉
            if (this.EnterNameString.Length == 0)
            {
                //this.HintObject.SetActive(true);
                this.SureObject.SetActive(false);
            }
            else
            {
                //this.HintObject.SetActive(false);
                this.SureObject.SetActive(true);
            }
        }

    }

    /// <summary>
    /// 名字輸入框文字與腳色姓名確認
    /// </summary>
    public void EnterStringCheck()
    {
        this.EnterNameString = RoleSelectController.script.CenterCard.gameObject.GetComponentInChildren<TextMesh>().text;

        if (this.EnterNameString.Length == 0)
        {
            //this.HintObject.SetActive(true);
            this.SureObject.SetActive(false);
        }
        else
        {
            //this.HintObject.SetActive(false);
            this.SureObject.SetActive(true);
        }
    }
}