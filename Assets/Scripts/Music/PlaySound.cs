using UnityEngine;
using System.Collections;

public class PlaySound : MonoBehaviour
{
    public SoundManager.SoundType SoundType;   //音樂類型

    // Use this for initialization
    void Start()
    {
        SoundManager.script.PlaySound(this.SoundType);
    }
}
