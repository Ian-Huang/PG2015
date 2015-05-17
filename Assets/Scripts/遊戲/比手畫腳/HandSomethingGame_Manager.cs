using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HandSomethingGame_Manager : MonoBehaviour
{
    public TextMesh QuestionText;

    public GameObject CorrectObject;
    public GameObject ErrorObject;

    public string CurrentQuestionString;

    [HideInInspector]
    public bool CanChooseButton;
    public static HandSomethingGame_Manager script;

    // Use this for initialization
    void Awake()
    {
        script = this;
    }

    void Start()
    {
        GameCollection.script.HandSomethingGameCorrectCount = 0;
        this.NextQuestion();
    }

    void NextQuestion()
    {
        //題目都使用過，則重新使用
        if (GameDefinition.HandSomethingGameQuestionList.Count == 0)
        {
            GameDefinition.HandSomethingGameQuestionList = new List<string>(GameDefinition.HandSomethingGameQuestionList_isUsed);
            GameDefinition.HandSomethingGameQuestionList_isUsed.Clear();
        }

        //題庫中隨機挑一題
        this.CurrentQuestionString = GameDefinition.HandSomethingGameQuestionList[Random.Range(0, GameDefinition.HandSomethingGameQuestionList.Count)];
        this.QuestionText.text = this.CurrentQuestionString;

        this.CanChooseButton = true;
    }

    /// <summary>
    /// 正確或放棄，以及顯示正錯或錯誤圖示
    /// </summary>
    /// <param name="isCorrect">true = 正確 , false = 放棄</param>
    public void StartShowResult(bool isCorrect)
    {
        //答對
        if (isCorrect)
        {
            //建立 圈圈 物件，停留時間1秒
            GameObject temp = Instantiate(this.CorrectObject) as GameObject;
            temp.transform.parent = this.transform;
            temp.GetComponent<AutoDestory>().AutoRunTime = 1;
            temp.SetActive(true);

            //正確音效
            SoundManager.script.PlaySound(SoundManager.SoundType.正確音效);

            StartCoroutine("RunShowAnswer", 1);
            GameCollection.script.HandSomethingGameCorrectCount++;
        }
        //放棄
        else
        {
            //建立 圈圈 物件，停留時間1.5秒
            GameObject temp = Instantiate(this.ErrorObject) as GameObject;
            temp.transform.parent = this.transform;
            temp.GetComponent<AutoDestory>().AutoRunTime = 1.5f;
            temp.SetActive(true);

            //錯誤音效
            SoundManager.script.PlaySound(SoundManager.SoundType.錯誤音效);

            StartCoroutine("RunShowAnswer", 1.5f);
        }

        //使用過題目不再使用
        GameDefinition.HandSomethingGameQuestionList_isUsed.Add(this.CurrentQuestionString);
        GameDefinition.HandSomethingGameQuestionList.Remove(this.CurrentQuestionString);

        this.CanChooseButton = false;
    }

    IEnumerator RunShowAnswer(float time)
    {
        yield return new WaitForSeconds(time);
        this.NextQuestion();
    }
}
