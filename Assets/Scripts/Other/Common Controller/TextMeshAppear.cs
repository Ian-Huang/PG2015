using UnityEngine;
using System.Collections;

/// <summary>
/// 通用函式，TextMesh 文件依序出現控制
/// </summary>
public class TextMeshAppear : MonoBehaviour
{
    public float AppearTime;            //過程花費時間
    public float DelayTime;             //延遲時間
    public EndToDo endToDo;             //文字完成後，確認要做甚麼事情
    //public bool isNextEvent = false;    //文字完成後是否進行事件切換 (EventCollection script)

    private bool isMouseActive;
    private bool isComplete;    //確認文字出現是否完成
    private TextMesh textMesh;
    public string ShowString;  //儲存字串

    void Awake()
    {
        this.textMesh = this.GetComponent<TextMesh>();
        this.ShowString = this.textMesh.text;
    }


    void OnEnable()
    {
        this.isComplete = false;
        this.textMesh.text = string.Empty;      //字串清空
        iTween.ValueTo(this.gameObject, iTween.Hash(
                "name", "TextAppear",
                "from", 0,
                "to", this.ShowString.Length,
                "time", this.AppearTime,
                "delay", this.DelayTime,
                "onupdate", "TextUpdate",
                "oncomplete", "TextComplete",
                "easetype", iTween.EaseType.linear));

        this.isMouseActive = false;
        StartCoroutine(this.CheckMouseActive(this.DelayTime));
    }

    /// <summary>
    /// 重設新字串，並重製動畫
    /// </summary>
    /// <param name="text">新文字</param>
    public void ResetText(string text)
    {
        this.isComplete = false;
        this.ShowString = text;
        this.textMesh.text = string.Empty;      //字串清空
        iTween.StopByName(this.gameObject, "TextAppear");
        iTween.ValueTo(this.gameObject, iTween.Hash(
                "name", "TextAppear",
                "from", 0,
                "to", this.ShowString.Length,
                "time", this.AppearTime,
                "delay", this.DelayTime,
                "onupdate", "TextUpdate",
                "oncomplete", "TextComplete",
                "easetype", iTween.EaseType.linear));

        this.isMouseActive = false;
        StartCoroutine(this.CheckMouseActive(this.DelayTime));
    }

    IEnumerator CheckMouseActive(float time)
    {
        yield return new WaitForSeconds(time);
        this.isMouseActive = true;
    }

    /// <summary>
    /// 文字出現Update函式
    /// </summary>
    /// <param name="value"></param>
    void TextUpdate(int value)
    {
        this.textMesh.text = this.ShowString.Substring(0, value);
    }

    /// <summary>
    /// 文字出現完成後呼叫
    /// </summary>
    void TextComplete()
    {
        this.isComplete = true;
    }

    void Update()
    {
        if (this.isMouseActive)
        {
            if (Input.GetMouseButtonDown(0))  //按下滑鼠左鍵
            {
                if (!this.isComplete)
                {
                    //狀態如未完成，停止ITween動畫並顯示所有文字
                    iTween.StopByName(this.gameObject, "TextAppear");
                    this.textMesh.text = this.ShowString;
                    this.isComplete = true;
                }
                else
                {
                    //根據不同的指定結尾來處理後續內容
                    switch (this.endToDo)
                    {
                        case EndToDo.Nothing:
                            break;
                        case EndToDo.NextEvent:
                            EventCollection.script.NextEvent(); //切換下一事件
                            //this.enabled = false;   //關閉此script，避免再度觸發
                            break;
                        case EndToDo.NPCTalkNextContent:
                            NPCTalkingManager.script.NextTalk();
                            break;
                        case EndToDo.EnterGame:
                            GameCollection.script.GameOpening();
                            this.enabled = false;   //關閉此script，避免再度觸發
                            break;
                        case EndToDo.ExitMission:
                            NPCTalkingManager.script.ExitMissionTalking();
                            this.enabled = false;   //關閉此script，避免再度觸發
                            break;
                        case EndToDo.TreasureActive:
                            GameDefinition.CurrentTreasureController_Script.CloseTreasure();
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }

    public enum EndToDo
    {
        Nothing = 0,
        NextEvent = 1,
        NPCTalkNextContent = 2, EnterGame = 3, ExitMission = 4,
        TreasureActive = 5
    }
}
