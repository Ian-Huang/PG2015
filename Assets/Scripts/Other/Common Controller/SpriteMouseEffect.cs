using UnityEngine;
using System.Collections;

public class SpriteMouseEffect : MonoBehaviour
{
    public Sprite NormalSprite;
    public Sprite ActiveSprite;

    void OnMouseDown()
    {
        this.GetComponent<SpriteRenderer>().sprite = this.ActiveSprite;
    }

    void OnMouseUp()
    {
        this.GetComponent<SpriteRenderer>().sprite = this.NormalSprite;
    }

    void OnEnable()
    {
        this.GetComponent<SpriteRenderer>().sprite = this.NormalSprite;
    }
}
