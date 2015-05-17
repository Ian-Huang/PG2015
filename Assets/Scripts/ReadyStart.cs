using UnityEngine;
using System.Collections;

public class ReadyStart : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            Application.LoadLevel("前導");
        }
    }
}
