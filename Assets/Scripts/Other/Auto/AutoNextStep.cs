using UnityEngine;
using System.Collections;

public class AutoNextStep : MonoBehaviour
{
    public HandleType handType;
    public float NextStepTime;

    // Use this for initialization
    IEnumerator Start()
    {
        yield return new WaitForSeconds(this.NextStepTime);
        if (this.handType == HandleType.GameCollection)
            GameCollection.script.NextGameStep();
        else if (this.handType == HandleType.EventCollection)
            EventCollection.script.NextEvent();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public enum HandleType
    {
        GameCollection = 1, EventCollection = 2
    }
}
