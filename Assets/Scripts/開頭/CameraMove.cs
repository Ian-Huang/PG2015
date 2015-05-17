using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour
{
    public float BeginCameraSize;
    public float EndCameraSize;

    public iTween.EaseType easeType;
    public float MoveTime;
    public float DelayTime;

    public static CameraMove script;

    void Awake()
    {
        script = this;
    }

    public void CameraStartMove()
    {
        iTween.MoveTo(this.gameObject, iTween.Hash(
             "position", Vector3.zero,
             "time", this.MoveTime,
             "easetype", this.easeType,
             "delay", this.DelayTime
             ));

        iTween.ValueTo(this.gameObject, iTween.Hash(
                "from", this.BeginCameraSize,
                "to", this.EndCameraSize,
                "time", this.MoveTime,
                "delay", this.DelayTime,
                "easetype", this.easeType,
                "onupdate", "CameraSizeUpdate"));
    }

    void CameraSizeUpdate(float value)
    {
        this.camera.orthographicSize = value;
    }
}
