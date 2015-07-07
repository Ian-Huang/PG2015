using UnityEngine;
using System.Collections;

public class JumpStroyButton : MonoBehaviour
{
    public int StoryIndex;

    void OnMouseEnter()
    {
        if (QuickJumpGame.script.isShowUI)
        {
            this.GetComponent<TextMesh>().color = QuickJumpGame.script.TextActiveColor;
        }
    }

    void OnMouseExit()
    {
        if (QuickJumpGame.script.isShowUI)
        {
            this.GetComponent<TextMesh>().color = QuickJumpGame.script.TextNormalColor;
        }
    }

    void OnMouseUpAsButton()
    {
        if (QuickJumpGame.script.isShowUI)
        {
            QuickJumpGame.script.SetUIVisible(false);
            GameDefinition.QuickJumpStoryIndex = this.StoryIndex;
            Application.LoadLevelAsync("PG2015");
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
