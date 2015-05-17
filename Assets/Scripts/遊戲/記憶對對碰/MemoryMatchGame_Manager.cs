using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MemoryMatchGame_Manager : MonoBehaviour
{
    public List<MemoryGameRoleData> RoleDataList; //腳色動畫資訊清單

    public TextMesh RoleText;

    public TextMesh TimerObject;
    public int TimerCount = 5;

    public List<GameObject> CardCollection;

    public State CurrentState;
    public GameObject targetMatchObject = null;

    [HideInInspector]
    public int currentPlayRoleIndex = -1;
    private GameObject currentCloneRoleAnimation;

    public static MemoryMatchGame_Manager script;

    // Use this for initialization
    void Start()
    {
        this.RoleText.text = "";

        //移除未被選擇的腳色，同時找出觸發此遊戲的腳色(firstRole)，將會當作第一位遊戲腳色
        List<MemoryGameRoleData> tempList = new List<MemoryGameRoleData>(this.RoleDataList);
        MemoryGameRoleData firstRole = null;
        foreach (MemoryGameRoleData temp in tempList)
        {
            if (GameDefinition.PlayerNameData[temp.SystemName] == string.Empty)
                this.RoleDataList.Remove(temp);
            else if (temp.SystemName == GameDefinition.CurrentChoosePlayerName)
                firstRole = temp;
        }
        //將觸發此遊戲的腳色移到清單第一位
        this.RoleDataList.Remove(firstRole);
        this.RoleDataList.Insert(0, firstRole);

        //將所有 Card 儲存至清單
        foreach (var temp in this.GetComponentsInChildren<MemoryMatchGame_Card>())
            this.CardCollection.Add(temp.gameObject);

        for (int i = 0; i < this.CardCollection.Count; i++)
        {
            int num = Random.Range(0, this.CardCollection.Count);
            Vector3 tempV3 = this.CardCollection[i].transform.position;
            this.CardCollection[i].transform.position = this.CardCollection[num].transform.position;
            this.CardCollection[num].transform.position = tempV3;
        }

        this.CurrentState = State.StopGame;

        //開始看牌倒數計時
        this.InvokeRepeating("Timer", 1, 1);
        this.TimerObject.text = this.TimerCount.ToString();
    }

    /// <summary>
    /// 看牌倒數計時器
    /// </summary>
    void Timer()
    {
        this.TimerCount--;
        if (this.TimerCount > 0)
            this.TimerObject.text = this.TimerCount.ToString();
        else if (this.TimerCount == 0)
        {
            this.StartGame();
            this.TimerObject.text = "GO";
        }
        else
        {
            this.TimerObject.text = "";
            this.CancelInvoke("Timer");
        }
    }

    /// <summary>
    /// 目前進行的遊戲腳色出現，並刪除前一位腳色
    /// </summary>
    public void RoleAppear()
    {
        if (this.currentCloneRoleAnimation != null)
            Destroy(this.currentCloneRoleAnimation);

        this.currentPlayRoleIndex++;
        if (this.currentPlayRoleIndex == this.RoleDataList.Count)
            this.currentPlayRoleIndex = 0;

        this.currentCloneRoleAnimation = Instantiate(this.RoleDataList[this.currentPlayRoleIndex].RoleObject) as GameObject;
        this.currentCloneRoleAnimation.SetActive(true);
        this.currentCloneRoleAnimation.transform.parent = this.transform;

        //顯示腳色名字於 腳色框中
        this.RoleText.text = GameDefinition.PlayerNameData[this.RoleDataList[this.currentPlayRoleIndex].SystemName];
    }

    /// <summary>
    /// 確認所有卡片是否都已翻開
    /// </summary>
    public void CheckCardOK()
    {
        bool isOK = true;
        foreach (var temp in this.GetComponentsInChildren<MemoryMatchGame_Card>())
        {
            if (temp.FaceType == MemoryMatchGame_Card.CardFaceType.Front && !temp.canRotate)
                continue;
            else
            {
                isOK = false;
                break;
            }
        }

        if (isOK)
        {
            //遊戲完成
            this.CurrentState = State.StopGame;

            //將各腳色分數紀錄於系統
            foreach (var temp in this.RoleDataList)
                GameCollection.script.MemoryGameRoleScoreMapping.Add(temp.SystemName, temp.score);

            //顯示下一階段 ， 統計成績(未完成)
            GameCollection.script.NextGameStep();
        }
    }

    /// <summary>
    /// 開始進行翻牌遊戲
    /// </summary>
    void StartGame()
    {
        //開始遊戲(測試)
        foreach (var temp in this.GetComponentsInChildren<MemoryMatchGame_Card>())
        {
            temp.canRotate = true;
            temp.RotateCard(MemoryMatchGame_Card.CardFaceType.Back);
        }

        this.RoleAppear();  //第一位腳色出現，開始遊戲
        this.CurrentState = State.StartGame;
    }

    void Awake()
    {
        script = this;
    }

    public enum State
    {
        StopGame = 0,   //遊戲停止
        StartGame = 1,  //遊戲開始
        Matching = 2,   //配對中
        Recover = 3     //卡片回復為反面的過程
    }

    [System.Serializable]
    public class MemoryGameRoleData : GameDefinition.RoleData
    {
        public int score;
    }
}