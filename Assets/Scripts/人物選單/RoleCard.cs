using UnityEngine;
using System.Collections;

public class RoleCard : MonoBehaviour
{
    public GameDefinition.SystemPlayerName SystemName;
    public int CurrentPositionIndex;
    public GameObject RoleObject;
    public iTween.EaseType easeType;

    public void ScaleTo(Vector3 target)
    {
        iTween.ScaleTo(this.gameObject, iTween.Hash("scale", target, "time", GameDefinition.CardChangeTime, "easetype", this.easeType));
    }

    public void MoveTo(Vector3 target)
    {
        iTween.MoveTo(this.gameObject, iTween.Hash("position", target, "time", GameDefinition.CardChangeTime, "easetype", this.easeType));
    }
}