using UnityEngine;
using System.Collections;

public class ChangeTouchEvent : MonoBehaviour
{
    public EventType eventType;

    // Use this for initialization
    void OnEnable()
    {
        if (this.eventType == EventType.Open)
        {
            foreach (TreasureController script in GameObject.FindObjectsOfType<TreasureController>())
                script.isCanTouch = true;

            foreach (NPC script in GameObject.FindObjectsOfType<NPC>())
                script.isCanTouch = true;
        }
        else
        {
            foreach (TreasureController script in GameObject.FindObjectsOfType<TreasureController>())
                script.isCanTouch = false;

            foreach (NPC script in GameObject.FindObjectsOfType<NPC>())
                script.isCanTouch = false;
        }
    }

    public enum EventType
    {
        Open = 0, Close = 1
    }
}