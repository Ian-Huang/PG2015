using UnityEngine;
using System.Collections;

public class AutoChangeScene : MonoBehaviour
{
    public string SceneName;
    public float DelayTime;

    // Use this for initialization
    IEnumerator Start()
    {
        yield return new WaitForSeconds(this.DelayTime);
        Application.LoadLevel(this.SceneName);
    }
}
