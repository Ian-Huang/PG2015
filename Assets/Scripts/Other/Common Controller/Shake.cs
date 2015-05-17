using UnityEngine;
using System.Collections;

public class Shake : MonoBehaviour
{
    public GameObject ShakeObject;
    public float ShakeTime;
    public float ShakeDelayTime;
    public Vector3 ShakeAmount;

    public bool isStartShake;

    // Use this for initialization
    void Start()
    {
        if (this.isStartShake)
        {
            //鏡頭震動
            iTween.ShakePosition(this.ShakeObject, iTween.Hash(
                "amount", this.ShakeAmount,
                "delay", this.ShakeDelayTime,
                "time", this.ShakeTime));
        }
    }
}