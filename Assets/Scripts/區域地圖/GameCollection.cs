using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameCollection : MonoBehaviour
{
    public List<GameData> GameDataList;

    [HideInInspector]
    public GameData CurrentGameData;  //當前遊戲資訊

    public int CurrentGameStepIndex = 0;    //紀錄遊戲流程索引值

    //紀錄"記憶對對碰"遊戲腳色成績
    public Dictionary<GameDefinition.SystemPlayerName, int> MemoryGameRoleScoreMapping = new Dictionary<GameDefinition.SystemPlayerName, int>();

    //紀錄"快問快答"遊戲成績
    [HideInInspector]
    public int QuickAnsGameCorrectCount = 0;

    //紀錄"比手畫腳"遊戲成績
    [HideInInspector]
    public int HandSomethingGameCorrectCount = 0;

    //紀錄"顏不及意"遊戲成績
    [HideInInspector]
    public int ColorGameCorrectCount = 0;

    //紀錄"推理在晚餐後"遊戲成績
    [HideInInspector]
    public int ReasoningGame_OneHintAnswerCount = 0;
    [HideInInspector]
    public int ReasoningGame_TwoHintAnswerCount = 0;
    [HideInInspector]
    public int ReasoningGame_ThreeHintAnswerCount = 0;
    [HideInInspector]
    public int ReasoningGame_FourHintAnswerCount = 0;

    public static GameCollection script;

    void Awake()
    {
        script = this;
    }

    /// <summary>
    /// 開始進行目前選定遊戲的開頭
    /// </summary>
    public void GameOpening()
    {
        //找出將進行遊戲類型的資料
        this.CurrentGameData = this.GameDataList.Find((GameData data) =>
        {
            return (data.Game_Type == GameDefinition.CurrentChooseGameType);
        });

        //開啟遊戲物件
        this.CurrentGameData.Game_Object.SetActive(true);

        // Check : 先將所有遊戲階段物件關閉
        foreach (var temp in this.CurrentGameData.GameStepList)
            temp.SetActive(false);

        //遊戲階段索引值從0開始
        this.CurrentGameStepIndex = 0;

        //開啟第一階段遊戲物件
        this.CurrentGameData.GameStepList[this.CurrentGameStepIndex].SetActive(true);
    }

    /// <summary>
    /// 目前遊戲進入下一階段
    /// </summary>
    public void NextGameStep()
    {
        this.CurrentGameData.GameStepList[this.CurrentGameStepIndex].SetActive(false);    //關閉前一事件物件
        this.CurrentGameStepIndex++;
        this.CurrentGameData.GameStepList[this.CurrentGameStepIndex].SetActive(true);     //開啟新一事件物件
    }

    [System.Serializable]
    public class GameData
    {
        public GameDefinition.GameType Game_Type;
        public GameObject Game_Object;
        public List<GameObject> GameStepList;
    }
}
