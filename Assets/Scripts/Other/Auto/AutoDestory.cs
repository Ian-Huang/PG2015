using UnityEngine;
using System.Collections;

public class AutoDestory : MonoBehaviour
{
    public float AutoRunTime;

    // Use this for initialization
    IEnumerator Start()
    {
        yield return new WaitForSeconds(this.AutoRunTime);
        Destroy(this.gameObject);
    }
}