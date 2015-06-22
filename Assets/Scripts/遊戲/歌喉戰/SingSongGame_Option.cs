using UnityEngine;
using System.Collections;

public class SingSongGame_Option : MonoBehaviour
{
    public bool isTureAnswer;

    public Sprite NormalSprite;
    public Sprite ActiveSprite;

    [HideInInspector]
    public bool isCanChoose = true;

    void OnMouseEnter()
    {
        if (this.isCanChoose)
            this.GetComponent<SpriteRenderer>().sprite = this.ActiveSprite;
    }

    void OnMouseExit()
    {
        if (this.isCanChoose)
            this.GetComponent<SpriteRenderer>().sprite = this.NormalSprite;
    }

    void OnMouseUpAsButton()
    {
        if (this.isCanChoose)
        {
            if (this.isTureAnswer)
                GameObject.FindObjectOfType<SingSongGame_Manager>().CheckAnswer(true);
            else
                GameObject.FindObjectOfType<SingSongGame_Manager>().CheckAnswer(false);
        }
    }
}
