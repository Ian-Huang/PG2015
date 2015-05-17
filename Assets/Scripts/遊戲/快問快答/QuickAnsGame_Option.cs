using UnityEngine;
using System.Collections;

public class QuickAnsGame_Option : MonoBehaviour
{
    public int OptionNumber;

    public int originFont;

    void OnMouseEnter()
    {
        if (QuickAnsGame_Manager.script.CanChoose)
        {
            this.GetComponent<TextMesh>().color = QuickAnsGame_Manager.script.OptionActiveColor;
            this.GetComponent<TextMesh>().fontSize = (int)(originFont * 1.05f);
        }
    }

    void OnMouseExit()
    {
        if (QuickAnsGame_Manager.script.CanChoose)
        {
            this.GetComponent<TextMesh>().color = QuickAnsGame_Manager.script.OptionNormalColor;
            this.GetComponent<TextMesh>().fontSize = originFont;
        }
    }

    void OnMouseUpAsButton()
    {
        if (QuickAnsGame_Manager.script.CanChoose)
        {
            //答對處理
            if (this.OptionNumber == QuickAnsGame_Manager.script.CurrentQuestionData.TrueAnswer)
            {
                QuickAnsGame_Manager.script.StartShowAnswer(true);
            }
            //答錯處理
            else
            {
                QuickAnsGame_Manager.script.StartShowAnswer(false);
            }
        }
    }

    void Start()
    {
        this.originFont = this.GetComponent<TextMesh>().fontSize;
    }
}