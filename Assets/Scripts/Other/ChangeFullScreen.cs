using UnityEngine;
using System.Collections;

public class ChangeFullScreen : MonoBehaviour
{
    public static ChangeFullScreen script;

    public bool isStartRun = false;
    public bool isFullScreen = true;

    void Awake()
    {
        script = this;
    }

    // Use this for initialization
    void Start()
    {
        if (GameDefinition.GameScreenHeight == -1)
            GameDefinition.GameScreenHeight = Screen.height;
        if (GameDefinition.GameScreenWidth == -1)
            GameDefinition.GameScreenWidth = Screen.width;

        if (this.isStartRun)
            this.ChangeScreen(this.isFullScreen);
    }

    public void ChangeScreen(bool isCheck)
    {
        Screen.SetResolution(GameDefinition.GameScreenWidth, GameDefinition.GameScreenHeight, isCheck);
    }
}
