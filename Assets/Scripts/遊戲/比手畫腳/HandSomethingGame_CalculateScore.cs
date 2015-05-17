using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HandSomethingGame_CalculateScore : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        this.GetComponent<TextMesh>().text = "答對" + GameCollection.script.HandSomethingGameCorrectCount + "題" + "\n" + "獲得" + GameCollection.script.HandSomethingGameCorrectCount * 100 + "分";
    }
}