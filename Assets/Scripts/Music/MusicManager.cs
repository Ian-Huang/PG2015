using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MusicManager : MonoBehaviour
{
    public List<MusicData> MusicDataList;   //音樂資料清單

    public bool isAudioChange;
    public static MusicManager script;

    void Awake()
    {
        if (script != null)
            if (script != this)
            {
                Destroy(this.gameObject);
                return;
            }

        script = this;
    }

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        if (this.audio == null)
            this.gameObject.AddComponent<AudioSource>();

        this.isAudioChange = false;
    }

    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="type">音效種類</param>
    public void PlaySound(MusicType type)
    {
        if (!this.isAudioChange)
        {
            //如果目前沒有播放音樂
            if (!this.audio.isPlaying)
            {
                if (type == MusicType.關閉)
                    return;

                this.audio.volume = 1;
                this.audio.clip = this.MusicDataList.Find((MusicData data) =>
                {
                    return (data.musicType == type);
                }).musicClip;

                this.audio.Play();
            }
            //有播放音樂，則進行淡入淡出
            else
            {
                this.isAudioChange = true;
                iTween.AudioTo(this.gameObject, iTween.Hash(
                            "audiosource", this.audio,
                            "volume", 0,
                            "time", 1,
                            "oncompleteparams", type,
                            "easetype", iTween.EaseType.linear,
                            "oncomplete", "AudioChangeComplete"
                        ));
            }
        }
    }

    void AudioChangeComplete(MusicType type)
    {
        if (type == MusicType.關閉)
        {
            this.isAudioChange = false;
            this.audio.clip = null;
            return;
        }

        this.audio.clip = this.MusicDataList.Find((MusicData data) =>
        {
            return (data.musicType == type);
        }).musicClip;

        this.isAudioChange = false;
        this.audio.Play();
        iTween.AudioTo(this.gameObject, iTween.Hash(
                        "audiosource", this.audio,
                        "volume", 1,
                        "easetype", iTween.EaseType.linear,
                        "time", 2
                    ));
    }

    public void ChangeLoopState(bool isLoop)
    {
        this.audio.loop = isLoop;
    }

    //void OnGUI()
    //{
    //    if (GUI.Button(new Rect(0, 0, 50, 50), "music 1"))
    //    {
    //        this.PlaySound(MusicType.關閉);
    //    }
    //    //if (GUI.Button(new Rect(0, 75, 50, 50), "music 2"))
    //    //{
    //    //    this.PlaySound(MusicType.正義的後繼);
    //    //}
    //}

    /// <summary>
    /// 音樂資料
    /// </summary>
    [System.Serializable]
    public class MusicData
    {
        public MusicType musicType; //音樂類型
        public AudioClip musicClip; //音樂片段
    }

    public enum MusicType
    {
        關閉 = 0, 開頭前言 = 1, 開頭影片音樂 = 2, 選角 = 3, 地圖轉換 = 4, 結尾影片音樂 = 8,
        //島嶼背景音樂
        莎吉斯島 = 5, 布列德島 = 6, 康費爾森島 = 7,

        //進行支線音樂
        //(智慧)莎吉斯島
        卡片掉了 = 11, 黃綠紅 = 12, 知識通 = 13, 推理要在晚餐後 = 14, 消失的羅盤 = 15,
        //(勇氣)布列德島
        奶油水果派 = 16, 給我食譜 = 17, 我的船壞了 = 18, 在我的歌聲裡 = 19, 你怎麼連話都說不清楚 = 20,
        //(自信)康費爾森島
        我要成為畢卡索 = 21, 筆墨登場 = 22, 你是我的眼 = 23, 未填詞 = 24, 混亂的程序 = 25,

        //遊戲配樂
        原來不是你 = 31, 妙筆生輝 = 32, 快打旋風 = 33, 眼名嘴快 = 34, 即刻救援 = 35, 瞻前顧後 = 36, 神機妙算 = 37, 顏不及意 = 38,

        //神秘島
        神秘島問題前 = 40, 神秘島問題進行中 = 41, 神秘島問題後 = 42,

        //歌喉戰音樂  SGI頌、正義的後繼
        SGI頌_Cut = 50, SGI頌 = 51, 正義的後繼_Cut = 52, 正義的後繼 = 53
    }
}
