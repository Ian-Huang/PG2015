using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Test : MonoBehaviour
{
    public float AppearTime;
    public string address = "";
    public string ShowString;
    public AudioClip clip;

    public GameObject ShinePlane;

    // Use this for initialization
    void Start()
    {
        //this.GetComponent<TextMesh>().text = date.Year + "." + date.Month + "." + date.Day + "\n" + "錦州會館";
        // print(date.Year + "." + date.Month + "." + date.Day);
    }

    // Update is called once per frame
    void Update()
    {

    }
   
    void OnGUI()
    {
        this.address = GUI.TextField(new Rect(100, 100, 100, 100), this.address);

        if (GUI.Button(new Rect(100, 200, 100, 100), "Test"))
        {
            System.DateTime date = System.DateTime.Now;

            this.ShowString = date.Year + "." + date.Month + "." + date.Day + "\n" + this.address;


            iTween.ValueTo(this.gameObject, iTween.Hash(
                "from", 0,
                "to", this.ShowString.Length,
                "time", this.AppearTime,
                "onupdate", "TextUpdate",
                "oncomplete", "TextComplete",
                "easetype", iTween.EaseType.linear));
        }
    }

    private int oldValue = 0;
    void TextUpdate(int value)
    {
        if (this.oldValue != value)
            this.audio.PlayOneShot(this.clip);

        this.oldValue = value;
        this.GetComponent<TextMesh>().text = this.ShowString.Substring(0, value);
    }

    void TextComplete()
    {
        StartCoroutine(ShowShine(1));
    }

    IEnumerator ShowShine(float time)
    {
        yield return new WaitForSeconds(time);
        iTween.ScaleTo(this.ShinePlane, iTween.Hash(
                "scale", new Vector3(25, 25, 25),
                "time", 2,
                "easetype", iTween.EaseType.easeInSine));
    }
}