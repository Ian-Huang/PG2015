using UnityEngine;
using System.Collections;

public class SceneChange : MonoBehaviour
{
    public string[] SceneArrary;

    public GUIStyle style;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        GUILayout.Label("遊戲選單：", this.style);
        int i = GUILayout.Toolbar(-1, this.SceneArrary);
        if (i != -1)
        {
            Application.LoadLevel(this.SceneArrary[i]);
        }

    }
}
