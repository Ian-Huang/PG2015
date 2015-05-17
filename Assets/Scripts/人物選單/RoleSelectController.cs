using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 角色選擇控制器(角色選擇場景主要控制者)
/// </summary>
public class RoleSelectController : MonoBehaviour
{
    public GameObject StartGameButton;

    public List<Transform> SaveRoleCardTransformList;

    private List<Vector3> roleCardPositionList;
    private List<Vector3> roleCarScaleList;

    [HideInInspector]
    public RoleCard CenterCard;

    private bool isChanging;
    private GameObject CurrentChooseRoleObject;

    public static RoleSelectController script;

    void Awake()
    {
        script = this;

        // !!!!!進入角色選擇場景後，先清空系統預存名字 (之後會進行比對，當名字為空，則不參與遊戲)!!!!!
        Dictionary<GameDefinition.SystemPlayerName, string> tempDir = new Dictionary<GameDefinition.SystemPlayerName, string>(GameDefinition.PlayerNameData);
        foreach (var temp in tempDir.Keys)
            GameDefinition.PlayerNameData[temp] = string.Empty;
    }

    // Use this for initialization
    void Start()
    {
        this.StartGameButton.SetActive(false);
        this.isChanging = false;

        //紀錄角色卡片的位置、大小資訊
        this.roleCardPositionList = new List<Vector3>();
        this.roleCarScaleList = new List<Vector3>();
        foreach (Transform temp in this.SaveRoleCardTransformList)
        {
            this.roleCardPositionList.Add(temp.position);
            this.roleCarScaleList.Add(temp.localScale);
        }

        //產生中間的卡片的人物
        foreach (RoleCard tempScript in GameObject.FindObjectsOfType<RoleCard>())
        {
            if (tempScript.CurrentPositionIndex == 3)
            {
                this.CenterCard = tempScript;
                this.CurrentChooseRoleObject = Instantiate(tempScript.RoleObject) as GameObject;
                break;
            }
        }
    }

    /// <summary>
    /// 人物選擇列，向右移動一張卡片
    /// </summary>
    public void RunRightCard()
    {
        if (!this.isChanging)
        {
            RoleCard newFirstCard = null;
            RoleCard newLastCard = null;

            foreach (RoleCard temp in this.GetComponentsInChildren<RoleCard>())
            {
                if (temp.CurrentPositionIndex == 6)
                {
                    newFirstCard = temp;    //紀錄新的第一張卡片資訊
                    temp.CurrentPositionIndex = 0;
                    temp.transform.position = this.roleCardPositionList[temp.CurrentPositionIndex];
                }
                else
                {
                    if (temp.CurrentPositionIndex == 5) //紀錄新的最後一張卡片資訊
                        newLastCard = temp;

                    temp.CurrentPositionIndex++;
                    temp.MoveTo(this.roleCardPositionList[temp.CurrentPositionIndex]);

                    //控制卡片縮放(中間放大)
                    if (temp.CurrentPositionIndex == 4)
                        temp.ScaleTo(this.roleCarScaleList[temp.CurrentPositionIndex]);
                    if (temp.CurrentPositionIndex == 3)
                    {
                        this.CenterCard = temp;
                        temp.ScaleTo(this.roleCarScaleList[temp.CurrentPositionIndex]);
                    }
                }
            }

            //新的第一張卡片要等於最後一張卡片(未完成)
            newFirstCard.gameObject.GetComponent<SpriteRenderer>().sprite = newLastCard.gameObject.GetComponent<SpriteRenderer>().sprite;
            newFirstCard.gameObject.GetComponentInChildren<TextMesh>().text = newLastCard.gameObject.GetComponentInChildren<TextMesh>().text;
            newFirstCard.SystemName = newLastCard.SystemName;
            newFirstCard.RoleObject = newLastCard.RoleObject;

            //產生新人物由場景外走進場景
            Destroy(this.CurrentChooseRoleObject);
            this.CurrentChooseRoleObject = Instantiate(this.CenterCard.RoleObject) as GameObject;

            //名字輸入框文字與腳色姓名確認
            GameObject.FindObjectOfType<RoleNameEnter>().EnterStringCheck();

            //船舵旋轉
            //RudderRotate.script.RightRotate();

            //切換狀態，卡片切換未完成時不可繼續切換
            this.isChanging = true;
            StartCoroutine(TimetoChangeBool(GameDefinition.CardChangeTime));
        }
    }

    /// <summary>
    /// 人物選擇列，向左移動一張卡片
    /// </summary>
    public void RunLeftCard()
    {
        if (!this.isChanging)
        {
            RoleCard newLastCard = null;
            RoleCard newFirstCard = null;

            foreach (RoleCard temp in this.GetComponentsInChildren<RoleCard>())
            {
                if (temp.CurrentPositionIndex == 0)
                {
                    newLastCard = temp;    //紀錄新的最後一張卡片資訊
                    temp.CurrentPositionIndex = 6;
                    temp.transform.position = this.roleCardPositionList[temp.CurrentPositionIndex];
                }
                else
                {
                    if (temp.CurrentPositionIndex == 1) //紀錄新的第一張卡片資訊
                        newFirstCard = temp;

                    temp.CurrentPositionIndex--;
                    temp.MoveTo(this.roleCardPositionList[temp.CurrentPositionIndex]);
                    //控制卡片縮放(中間放大)
                    if (temp.CurrentPositionIndex == 2)
                        temp.ScaleTo(this.roleCarScaleList[temp.CurrentPositionIndex]);
                    if (temp.CurrentPositionIndex == 3)
                    {
                        this.CenterCard = temp;
                        temp.ScaleTo(this.roleCarScaleList[temp.CurrentPositionIndex]);
                    }
                }
            }

            //新的最後一張卡片要等於第一張卡片(未完成)
            newLastCard.gameObject.GetComponent<SpriteRenderer>().sprite = newFirstCard.gameObject.GetComponent<SpriteRenderer>().sprite;
            newLastCard.gameObject.GetComponentInChildren<TextMesh>().text = newFirstCard.gameObject.GetComponentInChildren<TextMesh>().text;
            newLastCard.SystemName = newFirstCard.SystemName;
            newLastCard.RoleObject = newFirstCard.RoleObject;

            //產生新人物由場景外走進場景
            Destroy(this.CurrentChooseRoleObject);
            this.CurrentChooseRoleObject = Instantiate(this.CenterCard.RoleObject) as GameObject;

            //名字輸入框文字與腳色姓名確認
            GameObject.FindObjectOfType<RoleNameEnter>().EnterStringCheck();

            //船舵旋轉
            //RudderRotate.script.LeftRotate();

            //切換狀態，卡片切換未完成時不可繼續切換
            this.isChanging = true;
            StartCoroutine(TimetoChangeBool(GameDefinition.CardChangeTime));
        }
    }

    /// <summary>
    /// 紀錄玩家輸入的名字並儲存到系統
    /// </summary>
    public void SavePlayerNameToSystem()
    {
        string name = GameObject.FindObjectOfType<RoleNameEnter>().EnterNameString; //玩家輸入的名字字串

        //卡片出現輸入名字
        this.CenterCard.gameObject.GetComponentInChildren<TextMesh>().text = name;

        //將名字紀錄到系統
        GameDefinition.PlayerNameData[this.CenterCard.SystemName] = name;

        //判斷是否超過最小遊玩人數，顯示開始遊戲按鈕
        int count = 0;
        foreach (var temp in GameDefinition.PlayerNameData)
        {
            //假如為空，代表不使用此角色，將從畫面上刪除
            if (temp.Value != string.Empty)
            {
                count++;
            }
        }
        if (count >= GameDefinition.MinPlayerCount)
            this.StartGameButton.SetActive(true);
        else
            this.StartGameButton.SetActive(false);
    }

    /// <summary>
    /// 切換狀態，卡片切換未完成時不可繼續切換
    /// </summary>
    /// <param name="time">切換等待時間(s)</param>
    /// <returns></returns>
    IEnumerator TimetoChangeBool(float time)
    {
        yield return new WaitForSeconds(time);
        this.isChanging = false;
    }
}