using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuickAnsGame_Manager : MonoBehaviour
{
    public int EnterLineCount = 17; //每幾字換一行

    public TextMesh QuestionText;
    public TextMesh OptionText1;
    public TextMesh OptionText2;
    public TextMesh OptionText3;

    public Color OptionNormalColor;
    public Color OptionActiveColor;
    public Color CorrectOptionColor;

    public GameObject CorrectObject;
    public GameObject ErrorObject;

    [HideInInspector]
    public bool CanChoose;

    public GameDefinition.QuickAnsGameQuestionData CurrentQuestionData;

    public static QuickAnsGame_Manager script;

    // Use this for initialization
    void Awake()
    {
        script = this;
    }

    void Start()
    {
        GameCollection.script.QuickAnsGameCorrectCount = 0;
        this.NextQuestion();
    }

    void NextQuestion()
    {
        //題目都使用過，則重新使用
        if (GameDefinition.QuickAnsGameQuestionDataList.Count == 0)
        {
            GameDefinition.QuickAnsGameQuestionDataList = new List<GameDefinition.QuickAnsGameQuestionData>(GameDefinition.QuickAnsGameQuestionDataList_isUsed);
            GameDefinition.QuickAnsGameQuestionDataList_isUsed.Clear();
        }
        //題庫中隨機挑一題
        this.CurrentQuestionData = GameDefinition.QuickAnsGameQuestionDataList[Random.Range(0, GameDefinition.QuickAnsGameQuestionDataList.Count)];

        //題目字串特別處理，包含換行、換行後只有一次不換行等......
        string Qstring = this.CurrentQuestionData.QuestionText;
        string ShowString = "";

        //超出設定字數，處理換行
        if (Qstring.Length > this.EnterLineCount)
        {
            for (int count = 1; count <= Qstring.Length / this.EnterLineCount; count++)
            {
                //換行後只有一字，則不進行換行
                if (Qstring.Substring(this.EnterLineCount * count).Length == 1)
                    ShowString += Qstring.Substring(this.EnterLineCount * (count - 1));

                else
                {
                    //換行後面的字串如依然大於換行限定字數，則加入換行符號
                    if (Qstring.Substring(this.EnterLineCount * count).Length >= EnterLineCount)
                        ShowString += Qstring.Substring(this.EnterLineCount * (count - 1), this.EnterLineCount) + "\n";
                    //如沒超過限定字數，則將字串加於ShowString
                    else
                        ShowString += Qstring.Substring(this.EnterLineCount * (count - 1), this.EnterLineCount) + "\n" + Qstring.Substring(this.EnterLineCount * count);
                }
            }
        }
        else
            ShowString = Qstring;

        this.OptionText1.color = this.OptionNormalColor;
        this.OptionText2.color = this.OptionNormalColor;
        this.OptionText3.color = this.OptionNormalColor;

        //改變TextMesh Text (題目、選項一、選項二、選項三)
        this.QuestionText.text = ShowString;
        this.OptionText1.text = this.CurrentQuestionData.OptionText1;
        this.OptionText2.text = this.CurrentQuestionData.OptionText2;
        this.OptionText3.text = this.CurrentQuestionData.OptionText3;

        this.CanChoose = true;
    }

    /// <summary>
    /// 確認答案是否正確，以及顯示正錯或錯誤圖示
    /// </summary>
    /// <param name="isCorrect">true = 正確 , false = 錯誤</param>
    public void StartShowAnswer(bool isCorrect)
    {
        //選對答案
        if (isCorrect)
        {
            //建立 圈圈 物件，停留時間1秒
            GameObject temp = Instantiate(this.CorrectObject) as GameObject;
            temp.transform.parent = this.transform;
            temp.GetComponent<AutoDestory>().AutoRunTime = 1;
            temp.SetActive(true);

            //正確音效
            SoundManager.script.PlaySound(SoundManager.SoundType.正確音效);

            StartCoroutine(this.RunShowAnswer(1));
            GameCollection.script.QuickAnsGameCorrectCount++;   //計分+1
        }
        //選錯答案
        else
        {
            //建立 叉叉 物件，停留時間1.5秒
            GameObject temp = Instantiate(this.ErrorObject) as GameObject;
            temp.transform.parent = this.transform;
            temp.GetComponent<AutoDestory>().AutoRunTime = 1.5f;
            temp.SetActive(true);

            //錯誤音效
            SoundManager.script.PlaySound(SoundManager.SoundType.錯誤音效);

            StartCoroutine(this.RunShowAnswer(1.5f));
        }

        //使用過題目不再使用
        GameDefinition.QuickAnsGameQuestionDataList_isUsed.Add(this.CurrentQuestionData);
        GameDefinition.QuickAnsGameQuestionDataList.Remove(this.CurrentQuestionData);

        foreach (QuickAnsGame_Option script in this.GetComponentsInChildren<QuickAnsGame_Option>())
        {
            //顯示正確答案，並將錯誤答案 暫時隱藏
            if (script.OptionNumber == this.CurrentQuestionData.TrueAnswer)
                script.GetComponent<TextMesh>().color = this.CorrectOptionColor;
            else
                script.GetComponent<TextMesh>().color = new Color(0, 0, 0, 0);
        }

        this.CanChoose = false;
    }

    IEnumerator RunShowAnswer(float time)
    {
        yield return new WaitForSeconds(time);
        this.NextQuestion();
    }
}