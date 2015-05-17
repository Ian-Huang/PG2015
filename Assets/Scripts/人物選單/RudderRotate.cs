using UnityEngine;
using System.Collections;

public class RudderRotate : MonoBehaviour
{
    public Vector3 StartRotate;
    public Vector3 EndRotate;
    public float RotateTime;
    public iTween.EaseType easeType;

    public static RudderRotate script;

    void Awake()
    {
        script = this;
    }

    // Use this for initialization
    void Start()
    {
        this.RightRotate();
    }

    public void RightRotate()
    {
        this.transform.eulerAngles = Vector3.zero;
        iTween.RotateTo(this.gameObject, iTween.Hash(
                "z", this.transform.eulerAngles.z - 720,
                "time", this.RotateTime,
                "easetype", this.easeType));
    }

    public void LeftRotate()
    {
        this.transform.eulerAngles = Vector3.zero;
        iTween.RotateTo(this.gameObject, iTween.Hash(
                "z", this.transform.eulerAngles.z + 720,
                "time", this.RotateTime,
                "easetype", this.easeType));
    }
}
