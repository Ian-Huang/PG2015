using UnityEngine;
using System.Collections;

/// <summary>
/// 按一下滑鼠翻面
/// </summary>
public class MemoryMatchGame_Card : MonoBehaviour
{
    public Sprite Card_Front;
    public Sprite Card_Back;

    public CardFaceType FaceType;

    public float RotateTime;        //轉動時間
    public iTween.EaseType easeType;

    public bool isRotating; //正在轉卡 = true
    public bool canRotate;  //不能轉卡 = false

    void Start()
    {
        this.isRotating = false;
        this.canRotate = false;

        //確認 FaceType 狀態與卡片方向一致
        if (this.GetComponent<SpriteRenderer>().sprite == this.Card_Back)
            this.FaceType = CardFaceType.Back;
        else
            this.FaceType = CardFaceType.Front;
    }

    void OnMouseUpAsButton()
    {
        //確認是否可以轉動
        if (this.canRotate)
        {
            //確認MemoryMatchGame_Manager State
            if (MemoryMatchGame_Manager.script.CurrentState == MemoryMatchGame_Manager.State.StartGame)
            {
                //確認物體本身是否正在轉動
                if (!this.isRotating)
                {
                    //確認 MemoryMatchGame_Manager targetMatchObject 是否已經有對應物體
                    if (MemoryMatchGame_Manager.script.targetMatchObject == null)
                    {
                        MemoryMatchGame_Manager.script.targetMatchObject = this.gameObject;
                        this.RotateCard(CardFaceType.Front);
                    }
                    else
                    {
                        //確認 MemoryMatchGame_Manager targetMatchObject 是否為自身物體
                        if (MemoryMatchGame_Manager.script.targetMatchObject == this.gameObject)
                            return;

                        //Manager State 狀態改變為配對中
                        MemoryMatchGame_Manager.script.CurrentState = MemoryMatchGame_Manager.State.Matching;

                        this.RotateCard(CardFaceType.Front);
                    }
                }
            }
        }
    }

    #region Rotate Card

    /// <summary>
    /// 轉動卡片
    /// </summary>
    /// <param name="face">要轉動的方向</param>
    public void RotateCard(CardFaceType face)
    {
        //如要轉動的方面等於目前方向，則忽略轉動
        if (face == this.FaceType)
            return;

        iTween.RotateTo(this.gameObject, iTween.Hash("y", 90, "time", this.RotateTime, "easetype", this.easeType, "oncomplete", "RotateRun", "oncompleteparams", face));
        this.isRotating = true;
    }

    /// <summary>
    /// 轉動中繼點，更換圖片
    /// </summary>
    /// <param name="face"></param>
    void RotateRun(CardFaceType face)
    {
        //更換圖片
        if (face == CardFaceType.Back)
            this.GetComponent<SpriteRenderer>().sprite = this.Card_Back;
        else
            this.GetComponent<SpriteRenderer>().sprite = this.Card_Front;

        this.FaceType = face;
        iTween.RotateTo(this.gameObject, iTween.Hash("y", 0, "time", this.RotateTime, "easetype", this.easeType, "oncomplete", "RotateComplete"));
    }

    /// <summary>
    /// 轉動完成點
    /// </summary>
    void RotateComplete()
    {
        this.isRotating = false;    //修正轉動狀態

        //Manager state 為 Matching ， 進行配對確認
        if (MemoryMatchGame_Manager.script.CurrentState == MemoryMatchGame_Manager.State.Matching)
        {
            //確認與對應物件為不同物件
            if (this.gameObject != MemoryMatchGame_Manager.script.targetMatchObject)
            {
                //配對正確
                if (MemoryMatchGame_Manager.script.targetMatchObject.GetComponent<SpriteRenderer>().sprite == this.Card_Front)
                {
                    //將配對成功卡片設定為不可動物件
                    MemoryMatchGame_Manager.script.targetMatchObject.GetComponent<MemoryMatchGame_Card>().canRotate = false;
                    this.canRotate = false;

                    //MemoryMatchGame_Manager 狀態回復為初始
                    MemoryMatchGame_Manager.script.targetMatchObject = null;
                    MemoryMatchGame_Manager.script.CurrentState = MemoryMatchGame_Manager.State.StartGame;

                    //播放正確音效
                    SoundManager.script.PlaySound(SoundManager.SoundType.正確音效);

                    //針對目前進行腳色計分
                    MemoryMatchGame_Manager.script.RoleDataList[MemoryMatchGame_Manager.script.currentPlayRoleIndex].score++;

                    //檢查是否全部翻完
                    MemoryMatchGame_Manager.script.CheckCardOK();

                    //配對正確後....(未完成)
                }
                //配對錯誤
                else
                {
                    //將配對錯誤卡片翻回背面
                    MemoryMatchGame_Manager.script.targetMatchObject.GetComponent<MemoryMatchGame_Card>().RotateCard(CardFaceType.Back);
                    this.RotateCard(CardFaceType.Back);

                    //MemoryMatchGame_Manager 狀態設為 "回復中"
                    MemoryMatchGame_Manager.script.CurrentState = MemoryMatchGame_Manager.State.Recover;

                    //MemoryMatchGame_Manager 播放錯誤音效
                    SoundManager.script.PlaySound(SoundManager.SoundType.錯誤音效);

                    //切換下一位腳色進行遊戲
                    MemoryMatchGame_Manager.script.RoleAppear();

                    //配對錯誤後....(未完成)
                }
            }
        }
        //Manager state 為 Recover ， 進行回復卡片完成確認
        else if (MemoryMatchGame_Manager.script.CurrentState == MemoryMatchGame_Manager.State.Recover)
        {
            //MemoryMatchGame_Manager 狀態回復為初始
            MemoryMatchGame_Manager.script.targetMatchObject = null;
            MemoryMatchGame_Manager.script.CurrentState = MemoryMatchGame_Manager.State.StartGame;
        }
    }

    #endregion

    public enum CardFaceType
    {
        Back = 0, Front = 1
    }
}