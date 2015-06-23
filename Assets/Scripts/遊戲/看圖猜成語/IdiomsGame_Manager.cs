using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IdiomsGame_Manager : MonoBehaviour
{
    public GameObject ShowAnswerButtonObject;
    public GameObject TimerObject;

    public List<QuestionData> CardList;
    private List<QuestionData> currentCardList = new List<QuestionData>();
    public int currentQuestionIndex;

    public static IdiomsGame_Manager script;

    void Awake()
    {
        script = this;
    }

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

        this.currentQuestionIndex = 0;
        this.ShowQuestion();
    }

    public void ShowQuestion()
    {
        this.ShowAnswerButtonObject.SetActive(true);
        this.currentCardList[this.currentQuestionIndex].QuestionObject.SetActive(true);
    }

    public void ShowAnswer()
    {
        //秀解答
        this.currentCardList[this.currentQuestionIndex].AnswerObject.SetActive(true);

        this.currentQuestionIndex++;

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

        //時間到後結束遊戲
        if (this.TimerObject.GetComponent<GameTimer>().CountDownSecond <= 0)
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
}
