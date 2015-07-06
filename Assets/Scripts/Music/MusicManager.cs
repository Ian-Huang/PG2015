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
        關閉 = 0,

        //開頭、人物介紹、結尾
        開頭動畫 = 1, 人物介紹 = 2, 結尾 = 3,

        //故事音樂
        故事音樂1 = 11, 故事音樂2 = 12, 故事音樂3 = 13, 故事音樂4 = 14, 故事音樂5 = 15,
        故事音樂6 = 16, 故事音樂7 = 17, 故事音樂8 = 18, 故事音樂9 = 19, 故事音樂10 = 20,
        故事音樂11 = 21, 故事音樂12 = 22, 故事音樂13 = 23, 故事音樂14 = 24, 故事音樂15 = 25,

        //遊戲音樂
        文字繪畫聯想王 = 31, 即刻救援 = 32, 妙筆生輝 = 33, 快問快答 = 34, 肢體對對碰 = 35,
        看圖猜成語 = 36, 神機妙算 = 37, 記憶大考驗 = 38, 經典好歌 = 39,

        //歌喉戰音樂  SGI頌、正義的後繼、立下誓願的青年
        SGI頌_Cut = 50, SGI頌 = 51, 正義的後繼_Cut = 52, 正義的後繼 = 53, 立下誓願的青年_Cut = 54, 立下誓願的青年 = 55
    }
}
