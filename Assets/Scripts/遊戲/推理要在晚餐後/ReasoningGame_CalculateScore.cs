using UnityEngine;
using System.Collections;

public class ReasoningGame_CalculateScore : MonoBehaviour
{
    void Start()
    {
        int TotalScore =
            GameCollection.script.ReasoningGame_OneHintAnswerCount * 50 +
            GameCollection.script.ReasoningGame_TwoHintAnswerCount * 40 +
            GameCollection.script.ReasoningGame_ThreeHintAnswerCount * 30 +
            GameCollection.script.ReasoningGame_FourHintAnswerCount * 20;

        string showString = string.Format("使用一個提示答對{0}題\n使用兩個提示答對{1}題\n使用三個提示答對{2}題\n使用四個提示答對{3}題\n總共獲得{4}分"
            , GameCollection.script.ReasoningGame_OneHintAnswerCount, GameCollection.script.ReasoningGame_TwoHintAnswerCount,
            GameCollection.script.ReasoningGame_ThreeHintAnswerCount, GameCollection.script.ReasoningGame_FourHintAnswerCount, TotalScore);

        this.GetComponent<TextMesh>().text = showString;
    }
}
