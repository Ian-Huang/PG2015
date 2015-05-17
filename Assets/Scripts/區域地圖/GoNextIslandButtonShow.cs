using UnityEngine;
using System.Collections;

public class GoNextIslandButtonShow : MonoBehaviour
{
    public Sprite NormalSprite;
    public Sprite ActiveSprite;
    public Sprite ShineSprite;

    private bool CanChange = true;
    public float ChangeTime;

    void OnMouseDown()
    {
        this.GetComponent<SpriteRenderer>().sprite = this.ActiveSprite;
    }

    void OnMouseUp()
    {
        this.GetComponent<SpriteRenderer>().sprite = this.NormalSprite;
    }

    void OnMouseExit()
    {
        this.CanChange = true;
    }

    void OnMouseEnter()
    {
        CancelInvoke("ChangeSprite");
        this.CanChange = false;
    }

    void ChangeSprite()
    {
        if (this.GetComponent<SpriteRenderer>().sprite == this.NormalSprite)
            this.GetComponent<SpriteRenderer>().sprite = this.ShineSprite;
        else if (this.GetComponent<SpriteRenderer>().sprite == this.ShineSprite)
            this.GetComponent<SpriteRenderer>().sprite = this.NormalSprite;
    }

    void Update()
    {
        if (GameDefinition.CurrentGameTime <= 0)
        {
            if (this.CanChange)
                if (!IsInvoking("ChangeSprite"))
                    Invoke("ChangeSprite", this.ChangeTime);
        }
    }

    void OnEnable()
    {
        this.GetComponent<SpriteRenderer>().sprite = this.NormalSprite;
    }
}
