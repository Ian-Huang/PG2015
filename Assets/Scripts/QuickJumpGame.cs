using UnityEngine;
using System.Collections;

public class QuickJumpGame : MonoBehaviour
{
    public GameObject AllObjects;
    public bool isShowUI = false;

    public Color TextNormalColor;
    public Color TextActiveColor;

    public static QuickJumpGame script;

    void Awake()
    {
        script = this;
    }

    // Use this for initialization
    void Start()
    {
        this.AllObjects.SetActive(false);
    }

    public void SetUIVisible(bool isVisible)
    {
        if (isVisible)
        {
            if (!this.isShowUI)
            {
                this.isShowUI = true;
                Time.timeScale = 0;
                this.AllObjects.SetActive(true);
            }
        }
        else
        {
            if (this.isShowUI)
            {
                this.isShowUI = false;
                Time.timeScale = 1;
                this.AllObjects.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.J))
        {
            this.SetUIVisible(true);
        }
    }
}
