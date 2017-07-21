using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class FireController : MonoBehaviour
{
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] GameObject cigarette;

    /// <summary>
    /// 重置火
    /// </summary>
    public void OnFireAgain()
    {
        FirePartController[] firePartControllers = cigarette.GetComponentsInChildren<FirePartController>();
        for (int i = 0; i < firePartControllers.Length; i++)
        {
            firePartControllers[i].InitFire();
        }

        if (videoPlayer.isPlaying)
        {
            videoPlayer.Stop();
            videoPlayer.Play();
        }
        else
        {
            videoPlayer.Play();
        }
    }
}
