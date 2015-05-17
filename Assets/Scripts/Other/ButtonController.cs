using UnityEngine;
using System.Collections;

public class ButtonController : MonoBehaviour
{
    public GameDefinition.ButtonEvent buttonEvent;

    void OnMouseUpAsButton()
    {
        switch (this.buttonEvent)
        {
            case GameDefinition.ButtonEvent.ExitGame:
                Application.Quit();
                break;
            case GameDefinition.ButtonEvent.SureButton_RoleSelect:
                RoleSelectController.script.SavePlayerNameToSystem();
                RoleSelectController.script.RunRightCard();
                break;
            case GameDefinition.ButtonEvent.RightArrow_RoleSelect:
                RoleSelectController.script.RunRightCard();
                break;
            case GameDefinition.ButtonEvent.LeftArrow_RoleSelect:
                RoleSelectController.script.RunLeftCard();
                break;
            case GameDefinition.ButtonEvent.StartGame_RoleSelect:
                Application.LoadLevel("開頭海邊對話");
                break;
            case GameDefinition.ButtonEvent.MissionSure_Area:   //選擇任務確認:確定
                GameObject.FindObjectOfType<NPCTalkingManager>().ToMissionTalking();
                break;
            case GameDefinition.ButtonEvent.MissionCancel_Area: //選擇任務確認:取消
                EventCollection.script.BackEvent(); //退回前一事件(選NPC任務)
                GameDefinition.CurrentChooseMission = GameDefinition.Mission.None;
                //重設NPC狀態
                foreach (NPC script in GameObject.FindObjectsOfType<NPC>())
                    script.Reset();
                break;
            case GameDefinition.ButtonEvent.NextGameStep:  //(暫定) 遊戲規則的開始遊戲按鈕 (未來依不同遊戲可能要分開)
                GameCollection.script.NextGameStep();
                break;
            case GameDefinition.ButtonEvent.GameEnd:    //(暫定) 關閉目前正在進行遊戲的主體
                GameCollection.script.CurrentGameData.Game_Object.SetActive(false);
                NPCTalkingManager.script.NextTalk();
                break;
            case GameDefinition.ButtonEvent.GameEnd_卡片掉了:    //(暫定) 特別: (記憶對對碰)卡片掉了任務 ， 關閉目前正在進行遊戲的主體
                GameCollection.script.CurrentGameData.Game_Object.SetActive(false);
                NPCTalkingManager.script.NextTalk();
                //把卡片收集者的動作改為Idle
                NPCTalkingManager.script.CurrentTalkingData.NPCObject.GetComponent<SmoothMoves.BoneAnimation>().Play("idle");
                break;
            case GameDefinition.ButtonEvent.HandSomethingGame_Correct:    //比手畫腳，答對按鈕
                if (HandSomethingGame_Manager.script.CanChooseButton)
                    HandSomethingGame_Manager.script.StartShowResult(true);
                break;
            case GameDefinition.ButtonEvent.HandSomethingGame_Giveup:    //比手畫腳，放棄按鈕
                if (HandSomethingGame_Manager.script.CanChooseButton)
                    HandSomethingGame_Manager.script.StartShowResult(false);
                break;
            case GameDefinition.ButtonEvent.ColorGame_ShowAnswer:    //顏不及意，解答按鈕
                ColorGame_Manager.script.ShowAnswer();
                break;
            case GameDefinition.ButtonEvent.ColorGame_Correct:    //顏不及意，正確按鈕
                ColorGame_Manager.script.ShowQuestion();
                SoundManager.script.PlaySound(SoundManager.SoundType.正確音效);
                GameCollection.script.ColorGameCorrectCount++;
                break;
            case GameDefinition.ButtonEvent.ColorGame_Error:    //顏不及意，錯誤按鈕
                ColorGame_Manager.script.ShowQuestion();
                SoundManager.script.PlaySound(SoundManager.SoundType.錯誤音效);
                break;
            case GameDefinition.ButtonEvent.ReasoningGameNextHint:    //推理在晚餐後，下一提示
                ReasoningGame_Manager.script.ShowNextHint();
                break;
            case GameDefinition.ButtonEvent.ReasoningGameShowAnswer:    //推理在晚餐後，顯示答案
                ReasoningGame_Manager.script.ShowAnswer();
                break;
            case GameDefinition.ButtonEvent.TreasureGame_Finish:    //神秘島，寶物問題完成
                GameDefinition.CurrentTreasureController_Script.OpenEpilogue();
                break;

            case GameDefinition.ButtonEvent.NextAreaMap:    //進入結語，前往下一座島
                EventCollection.script.EventList[EventCollection.script.CurrentEventIndex].SetActive(false);    //關閉目前事件物件
                EventCollection.script.Special_CheckExitArea.SetActive(true);
                break;
            case GameDefinition.ButtonEvent.SureNextArea:    //防呆確認，進入結語，前往下一座島
                EventCollection.script.Special_CheckExitArea.SetActive(false);
                EventCollection.script.NextEvent(6);
                break;
            case GameDefinition.ButtonEvent.CancelNextArea:    //防呆取消，回到選NPC
                EventCollection.script.EventList[EventCollection.script.CurrentEventIndex].SetActive(true);    //關閉目前事件物件
                EventCollection.script.Special_CheckExitArea.SetActive(false);
                break;
            default:
                break;
        }
    }
}