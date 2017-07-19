using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class MoviePlayer : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    private void Start()
    {
        videoPlayer.Pause();
    }

    public void PlayFireMovie()
    {
        videoPlayer.Play();
    }
}
