using UnityEngine;
using System.Collections;

public class OpenMovieController : MonoBehaviour
{
    public MovieTexture MovieClip;

    // Use this for initialization
    IEnumerator Start()
    {
        this.renderer.material.mainTexture = this.MovieClip;
        this.MovieClip.loop = false;
        this.MovieClip.Play();
        yield return new WaitForSeconds(this.MovieClip.duration);

        CameraMove.script.CameraStartMove();
        EventCollection.script.NextEvent();

        iTween.ColorTo(this.gameObject, iTween.Hash("a", 0, "time", 1));
        yield return new WaitForSeconds(1);

    }
}
