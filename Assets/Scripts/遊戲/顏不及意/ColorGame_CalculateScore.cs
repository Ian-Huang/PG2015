using UnityEngine;
using System.Collections;

public class ColorGame_CalculateScore : MonoBehaviour
{
    private int totalScore;

    // Use this for initialization
    void Start()
    {
        this.GetComponent<TextMesh>().text = "答對" + GameCollection.script.ColorGameCorrectCount + "題" + "\n" + "獲得" + GameCollection.script.ColorGameCorrectCount * 50 + "分";
    }
}