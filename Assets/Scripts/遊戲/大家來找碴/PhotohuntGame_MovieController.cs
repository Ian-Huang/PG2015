using UnityEngine;
using System.Collections;

public class PhotohuntGame_MovieController : MonoBehaviour
{
    public MovieTexture MovieClip;

    // Use this for initialization
    IEnumerator Start()
    {
        this.renderer.material.mainTexture = this.MovieClip;
        this.MovieClip.Play();
        yield return new WaitForSeconds(this.MovieClip.duration + 1);
        GameCollection.script.NextGameStep();
    }
}