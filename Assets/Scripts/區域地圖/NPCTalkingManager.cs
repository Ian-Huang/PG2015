using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCTalkingManager : MonoBehaviour
{
    public GameObject BackgroundObject; //背景物件(將會根據不同任務作圖片切換)
    public float CameraMoveOffsetX;     //轉場Camera X 位置量

    public GameObject SpecialNPC_Chef;  //特殊NPC 甜點師(奶油水果派任務使用)
    public GameObject SpecialNPC_Sicker;  //特殊NPC 病人(你怎麼連話都說不清楚任務使用)

    public List<TalkingData> TalkingDataList;   //所有任務對話清單
    [HideInInspector]
    public TalkingData CurrentTalkingData;  //當前任務對話資訊
    public int CurrentTalkIndex = 0;    //紀錄目前對話索引值

    public static NPCTalkingManager script;

    public void ToMissionTalking()
    {
        //轉場
        iTween.MoveTo(Camera.main.gameObject, iTween.Hash(
                 "x", Camera.main.gameObject.transform.position.x + this.CameraMoveOffsetX,
                 "time", 1.25f,
                 "oncomplete", "MoveComplete",
                 "oncompleteparams", NPCTalkType.Enter,
                 "oncompletetarget", this.gameObject
                 ));

        //找出目前選擇任務的資訊
        this.CurrentTalkingData = this.TalkingDataList.Find((TalkingData data) =>
        {
            if (data.Mission == GameDefinition.CurrentChooseMission)
                return true;
            else
                return false;
        });

        //設定目前任務對話"背景"和"對話NPC"
        this.BackgroundObject.GetComponent<SpriteRenderer>().sprite = this.CurrentTalkingData.BackgroundSprite;
        this.CurrentTalkingData.NPCObject.SetActive(true);

        // --------特別任務：你怎麼連話都說不清楚 ------------
        if (this.CurrentTalkingData.Mission == GameDefinition.Mission.你怎麼連話都說不清楚)
            this.SpecialNPC_Sicker.SetActive(true);

        this.CurrentTalkIndex = 0;
    }

    public void ExitMissionTalking()
    {
        //轉場
        iTween.MoveTo(Camera.main.gameObject, iTween.Hash(
                 "x", Camera.main.gameObject.transform.position.x - this.CameraMoveOffsetX,
                 "time", 1.25f,
                 "oncomplete", "MoveComplete",
                 "oncompleteparams", NPCTalkType.Exit,
                 "oncompletetarget", this.gameObject
                 ));

        //切換音樂

        MusicChange MusicChangeScript = null;
        switch (GameDefinition.CurrentIsland)
        {
            case GameDefinition.Island.莎吉斯島:
                MusicChangeScript = this.gameObject.AddComponent<MusicChange>();
                MusicChangeScript.MusicType = MusicManager.MusicType.莎吉斯島;
                MusicChangeScript.isLoop = true;
                break;
            case GameDefinition.Island.布列德島:
                if (this.CurrentTalkingData.Mission != GameDefinition.Mission.在我的歌聲裡)   //歌喉戰音樂銜接較為特殊
                {
                    MusicChangeScript = this.gameObject.AddComponent<MusicChange>();
                    MusicChangeScript.MusicType = MusicManager.MusicType.布列德島;
                    MusicChangeScript.isLoop = true;
                }
                break;
            case GameDefinition.Island.康費爾森島:
                if (this.CurrentTalkingData.Mission != GameDefinition.Mission.未填詞)      //歌喉戰音樂銜接較為特殊
                {
                    MusicChangeScript = this.gameObject.AddComponent<MusicChange>();
                    MusicChangeScript.MusicType = MusicManager.MusicType.康費爾森島;
                    MusicChangeScript.isLoop = true;
                }
                break;
            default:
                break;
        }

        //將目前即將結束任務，至遊戲系統更新為"已進行" = true
        GameDefinition.MissionActiveStateMapping[GameDefinition.CurrentChooseMission] = true;

        //關閉當前任務所有對話內容
        this.CurrentTalkingData.TalkContentList[0].transform.parent.gameObject.SetActive(false);

        //往下進行下一個NPC事件 EventCollection NextEvent
        EventCollection.script.NextEvent();
    }

    void MoveComplete(NPCTalkType type)
    {
        //在進入任務對話轉場完成後
        if (type == NPCTalkType.Enter)
        {
            //假如有旁白，對話框與腳色同時出現
            if (this.CurrentTalkingData.Mission == GameDefinition.Mission.卡片掉了)
                this.NextTalk();

            //Role 出場 
            RoleAnimationCollection.script.RoleAppear(GameDefinition.CurrentChoosePlayerName);
        }
        //在離開任務對話轉場完成後
        else
        {
            //1.支線任務結束 or 15分鐘時間超過後，往下一事件進行  EventCollection NextEvent (未完成)
            switch (GameDefinition.CurrentIsland)
            {
                case GameDefinition.Island.莎吉斯島:
                    if (GameDefinition.MissionActiveStateMapping[GameDefinition.Mission.卡片掉了] &&
                        GameDefinition.MissionActiveStateMapping[GameDefinition.Mission.黃綠紅] &&
                        GameDefinition.MissionActiveStateMapping[GameDefinition.Mission.知識通] &&
                        GameDefinition.MissionActiveStateMapping[GameDefinition.Mission.推理要在晚餐後] &&
                        GameDefinition.MissionActiveStateMapping[GameDefinition.Mission.消失的羅盤]
                        )
                    {
                        EventCollection.script.NextEvent();
                        return;
                    }
                    break;
                case GameDefinition.Island.布列德島:
                    if (GameDefinition.MissionActiveStateMapping[GameDefinition.Mission.奶油水果派] &&
                        GameDefinition.MissionActiveStateMapping[GameDefinition.Mission.給我食譜] &&
                        GameDefinition.MissionActiveStateMapping[GameDefinition.Mission.我的船壞了] &&
                        GameDefinition.MissionActiveStateMapping[GameDefinition.Mission.在我的歌聲裡] &&
                        GameDefinition.MissionActiveStateMapping[GameDefinition.Mission.你怎麼連話都說不清楚]
                        )
                    {
                        EventCollection.script.NextEvent();
                        return;
                    }
                    break;
                case GameDefinition.Island.康費爾森島:
                    if (GameDefinition.MissionActiveStateMapping[GameDefinition.Mission.我要成為畢卡索] &&
                        GameDefinition.MissionActiveStateMapping[GameDefinition.Mission.筆墨登場] &&
                        GameDefinition.MissionActiveStateMapping[GameDefinition.Mission.你是我的眼] &&
                        GameDefinition.MissionActiveStateMapping[GameDefinition.Mission.未填詞] &&
                        GameDefinition.MissionActiveStateMapping[GameDefinition.Mission.混亂的程序]
                        )
                    {
                        EventCollection.script.NextEvent();
                        return;
                    }
                    break;
                default:
                    break;
            }


            //2.支線任務尚未結束，重載場景，讓所有物件還原。同時，紀錄目前剩餘的島嶼時間
            int countdownTime = GameObject.FindObjectOfType<GameTimer>().CountDownSecond;
            if (countdownTime <= 0)
                GameDefinition.CurrentGameTime = 0;
            else
                GameDefinition.CurrentGameTime = countdownTime;

            Application.LoadLevel(Application.loadedLevelName);
        }
    }

    /// <summary>
    /// 呼叫下一句對話框
    /// </summary>
    public void NextTalk()
    {
        if (this.CurrentTalkIndex != 0)
        {
            // --------特別任務：奶油水果派 ------------
            // 在貪吃鬼講完話後觸發
            if (GameDefinition.CurrentChooseMission == GameDefinition.Mission.奶油水果派 && this.CurrentTalkIndex == 1)
            {
                //開啟甜點師
                this.SpecialNPC_Chef.SetActive(true);
                //旋轉甜點師
                iTween.RotateTo(this.SpecialNPC_Chef, iTween.Hash(
                    "y", 2160,
                    "time", 1,
                    "easetype", iTween.EaseType.easeOutQuad
                ));
            }
            // --------特別任務：奶油水果派 ------------

            this.CurrentTalkingData.TalkContentList[this.CurrentTalkIndex - 1].SetActive(false);   //關閉前一事件物件            
            this.CurrentTalkingData.TalkContentList[this.CurrentTalkIndex].SetActive(true);        //開啟新一事件物件
            this.CurrentTalkIndex++;
        }
        else
        {
            this.CurrentTalkingData.TalkContentList[this.CurrentTalkIndex].SetActive(true);        //開啟新一事件物件
            this.CurrentTalkIndex++;
        }
    }

    void Awake()
    {
        script = this;
    }

    [System.Serializable]
    public class TalkingData
    {
        public GameDefinition.Mission Mission;
        public GameObject NPCObject;
        public Sprite BackgroundSprite;
        public List<GameObject> TalkContentList;
    }

    public enum NPCTalkType
    {
        Enter, Exit
    }
}