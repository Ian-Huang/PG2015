using UnityEngine;
using System.Collections;

public class ReadyStart : MonoBehaviour
{
    private bool isChangeScene = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (!this.isChangeScene)
            {
                this.isChangeScene = true;
                GameDefinition.QuickJumpStoryIndex = 0;
                Application.LoadLevelAsync("PG2015");
            }
        }
    }
}
