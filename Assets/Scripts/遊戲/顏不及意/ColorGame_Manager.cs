using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ColorGame_Manager : MonoBehaviour
{
    public TextMesh ColorText;
    public SpriteRenderer ColorBackground;
    public TextMesh AnswerText;

    public List<ColorData> ColorDataList;

    public GameObject Step1_ShowQuestion;
    public GameObject Step2_ShowAnswer;

    private int currentBackgroundColorValueIndex = -1;  //底色
    private int currentWordColorNameIndex = -1;         //字義
    private int currentWordColorValueIndex = -1;        //字色
    public string AnswerString;
    public static ColorGame_Manager script;

    void Awake()
    {
        script = this;
    }
    // Use this for initialization
    void Start()
    {
        GameCollection.script.ColorGameCorrectCount = 0;
        this.ShowQuestion();
    }

    public void ShowAnswer()
    {
        this.Step1_ShowQuestion.SetActive(false);
        this.Step2_ShowAnswer.SetActive(true);

        this.AnswerText.text = this.AnswerString;
    }

    public void ShowQuestion()
    {
        this.Step1_ShowQuestion.SetActive(true);
        this.Step2_ShowAnswer.SetActive(false);

        this.CreateQuestion();
    }

    void CreateQuestion()
    {
        //底色、字義、字色
        //底色 不能跟 字色相同
        //顏色不能跟上次一樣

        //字義：隨機挑選一顏色，不與上次重複
        int wordNameNum;
        do
        {
            wordNameNum = Random.Range(0, this.ColorDataList.Count);
        } while (wordNameNum == this.currentWordColorNameIndex);
        this.currentWordColorNameIndex = wordNameNum;

        //底色：隨機挑選一顏色，不與上次重複
        int backgroundColorNum;
        do
        {
            backgroundColorNum = Random.Range(0, this.ColorDataList.Count);
        } while (backgroundColorNum == this.currentBackgroundColorValueIndex);
        this.currentBackgroundColorValueIndex = backgroundColorNum;

        //字色：隨機挑選一顏色，不與上次重複且不與底色相同
        int wordColorValueNum;
        do
        {
            wordColorValueNum = Random.Range(0, this.ColorDataList.Count);
        } while (wordColorValueNum == this.currentBackgroundColorValueIndex || wordColorValueNum == this.currentWordColorValueIndex || wordColorValueNum == this.currentWordColorNameIndex);
        this.currentWordColorValueIndex = wordColorValueNum;

        //底色
        this.ColorBackground.color = this.ColorDataList[this.currentBackgroundColorValueIndex].ColorValue;
        //字義
        this.ColorText.text = this.ColorDataList[this.currentWordColorNameIndex].ColorName;
        //字色
        this.ColorText.color = this.ColorDataList[this.currentWordColorValueIndex].ColorValue;

        //解答
        this.AnswerString = this.ColorDataList[this.currentBackgroundColorValueIndex].ColorName + this.ColorDataList[this.currentWordColorNameIndex].ColorName + this.ColorDataList[this.currentWordColorValueIndex].ColorName;
    }

    [System.Serializable]
    public class ColorData
    {
        public string ColorName;
        public Color ColorValue;
    }
}