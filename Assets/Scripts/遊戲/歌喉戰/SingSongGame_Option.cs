using UnityEngine;
using System.Collections;

public class SingSongGame_Option : MonoBehaviour
{
    public bool isTureAnswer;

    public Color NormalColor;
    public Color ActiveColor;

    [HideInInspector]
    public bool isCanChoose = true;

    void OnMouseEnter()
    {
        if (this.isCanChoose)
            this.GetComponent<TextMesh>().color = this.ActiveColor;
    }

    void OnMouseExit()
    {
        if (this.isCanChoose)
            this.GetComponent<TextMesh>().color = this.NormalColor;
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
