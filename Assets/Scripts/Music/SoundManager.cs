using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 音效管理者，控制所有遊戲的音效
/// </summary>
public class SoundManager : MonoBehaviour
{
    public List<SoundData> SoundDataList;   //音效資料清單

    public AudioClip PlayClip;
    public float delayTime;

    public static SoundManager script;

    void Awake()
    {
        script = this;
    }

    // Use this for initialization
    void Start()
    {
        if (this.audio == null)
            this.gameObject.AddComponent<AudioSource>();

        if (this.PlayClip != null)
        {
            this.audio.clip = this.PlayClip;
            this.audio.PlayDelayed(this.delayTime);
        }
    }

    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="type">音效種類</param>
    public void PlaySound(SoundType type)
    {
        AudioClip tempClip = this.SoundDataList.Find((SoundData data) =>
         {
             return (data.soundType == type);
         }).soundClip;

        this.audio.PlayOneShot(tempClip);
    }

    /// <summary>
    /// 音效資料
    /// </summary>
    [System.Serializable]
    public class SoundData
    {
        public SoundType soundType; //音效類型
        public AudioClip soundClip; //音效片段
    }

    public enum SoundType
    {
        正確音效 = 1, 錯誤音效 = 2, 碰撞音效 = 3
    }
}
