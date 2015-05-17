using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SingSongGame_Manager : MonoBehaviour
{
    public List<SongTextData> SongTextDataList;

    public GameObject CorrectObject;
    public GameObject ErrorObject;
    public GameObject AgainObject;
    
    private int currentIndex = 0;

    // Use this for initialization
    void Start()
    {
        foreach (var temp in this.SongTextDataList)
            temp.TextObject.SetActive(false);

        this.StartGame();
    }

    void StartGame()
    {
        //this.audio.Play();
        this.SongTextDataList[this.currentIndex].TextObject.SetActive(true);
        Invoke("ShowSongText", this.SongTextDataList[this.currentIndex].Time);
    }

    void ShowSongText()
    {
        if (this.currentIndex >= this.SongTextDataList.Count - 1)
            return;

        this.SongTextDataList[this.currentIndex].TextObject.SetActive(false);
        this.currentIndex++;
        this.SongTextDataList[this.currentIndex].TextObject.SetActive(true);
        Invoke("ShowSongText", this.SongTextDataList[this.currentIndex].Time);
    }

    public void CheckAnswer(bool isCorrect)
    {
        if (isCorrect)
        {
            SoundManager.script.PlaySound(SoundManager.SoundType.正確音效);

            //建立 圈圈 物件，停留時間1.5秒
            GameObject temp = Instantiate(this.CorrectObject) as GameObject;
            temp.SetActive(true);
        }
        else
        {
            SoundManager.script.PlaySound(SoundManager.SoundType.錯誤音效);

            //建立 叉叉 物件，停留時間1.5秒
            GameObject temp = Instantiate(this.ErrorObject) as GameObject;
            temp.SetActive(true);
        }

        foreach (SingSongGame_Option script in this.GetComponentsInChildren<SingSongGame_Option>())
        {
            //顯示正確答案，並將錯誤答案 暫時隱藏
            if (script.isTureAnswer)
                script.GetComponent<TextMesh>().color = new Color(1, 0, 0, 1);
            else
                script.GetComponent<TextMesh>().color = new Color(0, 0, 0, 0);

            script.isCanChoose = false;
        }

        this.AgainObject.SetActive(true);
    }

    [System.Serializable]
    public class SongTextData
    {
        public GameObject TextObject;
        public float Time;
    }
}
