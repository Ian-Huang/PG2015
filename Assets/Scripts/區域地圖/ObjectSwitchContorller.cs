using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 控制被選擇物件開啟或關閉狀態
/// </summary>
public class ObjectSwitchContorller : MonoBehaviour
{
    public SwitchType switchType;           //開關類型
    public List<GameObject> ObjectList;     //將被控制的物件清單

    // Use this for initialization
    void OnEnable()
    {
        //確認狀態
        if (this.switchType == SwitchType.Open)
        {
            foreach (GameObject temp in this.ObjectList)
                temp.SetActive(true);
        }
        else
        {
            foreach (GameObject temp in this.ObjectList)
                temp.SetActive(false);
        }
    }

    public enum SwitchType
    {
        Open = 1, Close = 2
    }
}
