using UnityEngine;
using System.Collections;

public class FindDifferentGame_CheckArea : MonoBehaviour
{
    public AreaType Areatype;

    public bool AlreadyFind;  //已經被找到(觸發)

    void OnMouseUpAsButton()
    {
        //確認是否已經被找到
        if (!this.AlreadyFind)
        {
            if (this.Areatype == AreaType.Correct)
            {
                //播放正確音效
                SoundManager.script.PlaySound(SoundManager.SoundType.正確音效);
                this.GetComponentInChildren<MoveTo>().Move();
                this.AlreadyFind = true;
            }
            else
            {
                //播放錯誤音效
                SoundManager.script.PlaySound(SoundManager.SoundType.錯誤音效);
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        this.AlreadyFind = false;
    }

    public enum AreaType
    {
        Correct = 0, Error = 1
    }
}
