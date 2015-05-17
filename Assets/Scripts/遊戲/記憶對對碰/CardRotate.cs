using UnityEngine;
using System.Collections;

/// <summary>
/// 按一下滑鼠翻面
/// </summary>
public class CardRotate : MonoBehaviour
{
    public Sprite Card_Front;
    public Sprite Card_Back;

    public CardFaceType FaceType;

    public float RotateTime;
    public iTween.EaseType easeType;

    public bool isRotate;


    void Start()
    {
        this.isRotate = false;

        if (this.GetComponent<SpriteRenderer>().sprite == this.Card_Back)
            this.FaceType = CardFaceType.Back;
        else
            this.FaceType = CardFaceType.Front;
    }

    void OnMouseUpAsButton()
    {
        if (!this.isRotate)
        {
            if (this.FaceType == CardFaceType.Back)
                this.RotateCard(CardFaceType.Front);
            else
                this.RotateCard(CardFaceType.Back);
        }
    }

    #region Rotate Card
    void RotateCard(CardFaceType face)
    {
        if (face == this.FaceType)
            return;

        iTween.RotateTo(this.gameObject, iTween.Hash("y", 90, "time", this.RotateTime, "easetype", this.easeType, "oncomplete", "RotateRun", "oncompleteparams", face));
        this.isRotate = true;
    }

    void RotateRun(CardFaceType face)
    {
        if (face == CardFaceType.Back)
            this.GetComponent<SpriteRenderer>().sprite = this.Card_Back;
        else
            this.GetComponent<SpriteRenderer>().sprite = this.Card_Front;

        this.FaceType = face;

        iTween.RotateTo(this.gameObject, iTween.Hash("y", 0, "time", this.RotateTime, "easetype", this.easeType, "oncomplete", "RotateComplete"));
    }

    void RotateComplete()
    {
        this.isRotate = false;
    }

    #endregion

    public enum CardFaceType
    {
        Back = 0, Front = 1
    }
}