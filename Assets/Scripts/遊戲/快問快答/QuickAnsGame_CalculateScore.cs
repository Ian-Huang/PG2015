using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuickAnsGame_CalculateScore : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        if (GameCollection.script.QuickAnsGameCorrectCount <= 8)
            this.GetComponent<TextMesh>().text = "答對" + GameCollection.script.QuickAnsGameCorrectCount + "題" + "\n" + "獲得" + 300 + "分";
        else if (GameCollection.script.QuickAnsGameCorrectCount <= 16)
            this.GetComponent<TextMesh>().text = "答對" + GameCollection.script.QuickAnsGameCorrectCount + "題" + "\n" + "獲得" + 400 + "分";
        else
            this.GetComponent<TextMesh>().text = "答對" + GameCollection.script.QuickAnsGameCorrectCount + "題" + "\n" + "獲得" + 500 + "分";
    }
}