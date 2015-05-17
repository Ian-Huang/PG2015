using UnityEngine;
using System.Collections;

public class NormalMovieController : MonoBehaviour
{
    public MovieTexture MovieClip;

    public GameObject EndOpenObject;

    // Use this for initialization
    IEnumerator Start()
    {
        this.renderer.material.mainTexture = this.MovieClip;
        this.MovieClip.loop = false;
        this.MovieClip.Play();
        yield return new WaitForSeconds(this.MovieClip.duration);
        this.MovieClip.Stop();
        this.EndOpenObject.SetActive(true);
    }
}
