using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ReasoningGame_Manager : MonoBehaviour
{
    public GameObject NextHintButtonObject;
    public GameObject ShowAnswerButtonObject;

    public List<QuestionData> CardList;
    private List<QuestionData> currentCardList = new List<QuestionData>();
    public int currentQuestionIndex;

    public CardType CurrentCardType = CardType.None;

    public bool isRotating = false; //正在轉卡 = true

    public static ReasoningGame_Manager script;

    // Use this for initialization
    void Start()
    {
        //題目順序隨機打散
        for (int i = 0; i < this.CardList.Count; i++)
        {
            int random;
            do
            {
                random = Random.Range(0, this.CardList.Count);
            }
            while (this.currentCardList.Contains(this.CardList[random]));

            this.currentCardList.Add(this.CardList[random]);
        }

        GameCollection.script.ReasoningGame_OneHintAnswerCount = 0;
        GameCollection.script.ReasoningGame_TwoHintAnswerCount = 0;
        GameCollection.script.ReasoningGame_ThreeHintAnswerCount = 0;
        GameCollection.script.ReasoningGame_FourHintAnswerCount = 0;
        this.currentQuestionIndex = 0;
        this.isRotating = false;
        this.ShowQuestion();
    }
    void Awake()
    {
        script = this;
    }

    public void ShowQuestion()
    {
        this.NextHintButtonObject.SetActive(true);
        this.ShowAnswerButtonObject.SetActive(true);
        this.CurrentCardType = CardType.A;
        this.currentCardList[this.currentQuestionIndex].QuestionObject.SetActive(true);
        this.currentCardList[this.currentQuestionIndex].QuestionObject.GetComponentsInChildren<ReasoningGame_Card>().ToList().Find((ReasoningGame_Card data) =>
        {
            return (data.cardType == this.CurrentCardType);
        }).RotateCard();
    }

    public void ShowNextHint()
    {
        if (!this.isRotating)
        {
            this.CurrentCardType++;
            if (this.CurrentCardType == CardType.D)
            {
                this.NextHintButtonObject.SetActive(false);
            }

            this.currentCardList[this.currentQuestionIndex].QuestionObject.GetComponentsInChildren<ReasoningGame_Card>().ToList().Find((ReasoningGame_Card data) =>
            {
                return (data.cardType == this.CurrentCardType);
            }).RotateCard();
        }
    }

    public void ShowAnswer()
    {
        //計分
        switch (this.CurrentCardType)
        {
            case CardType.A:
                GameCollection.script.ReasoningGame_OneHintAnswerCount++;
                break;
            case CardType.B:
                GameCollection.script.ReasoningGame_TwoHintAnswerCount++;
                break;
            case CardType.C:
                GameCollection.script.ReasoningGame_ThreeHintAnswerCount++;
                break;
            case CardType.D:
                GameCollection.script.ReasoningGame_FourHintAnswerCount++;
                break;
        }

        //秀解答
        this.currentCardList[this.currentQuestionIndex].AnswerObject.SetActive(true);

        this.currentQuestionIndex++;

        this.NextHintButtonObject.SetActive(false);
        this.ShowAnswerButtonObject.SetActive(false);

        Invoke("NextQuestion", 2);
    }

    void NextQuestion()
    {
        //如果題目出完則進入結算介面
        if (this.currentQuestionIndex == this.currentCardList.Count)
        {
            GameCollection.script.NextGameStep();
            return;
        }

        this.currentCardList[this.currentQuestionIndex - 1].QuestionObject.SetActive(false);
        this.ShowQuestion();
    }

    [System.Serializable]
    public class QuestionData
    {
        public GameObject QuestionObject;
        public GameObject AnswerObject;
    }

    public enum CardType
    {
        None = 0, A = 1, B = 2, C = 3, D = 4
    }
}
