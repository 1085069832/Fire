using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateFire : MonoBehaviour
{
    TimeManager timeManager;
    MoviePlayer moviePlayer;
    [SerializeField] ParticleSystem glowXsm;//黄烟雾
    [SerializeField] ParticleSystem glowSm;//
    [SerializeField] ParticleSystem smokeXsm;//黑烟雾
    [SerializeField] ParticleSystem smokeSm;
    [SerializeField] ParticleSystem fireXsm;//火
    [SerializeField] ParticleSystem fireSm;
    [SerializeField] ParticleSystem fireMd;
    [SerializeField] ParticleSystem fireSpark;//火星
    [SerializeField] ParticleSystem cigarettePart;//烟头烟雾
    [SerializeField] GameObject[] groundFires;//地板火


    // Use this for initialization
    void Start()
    {
        timeManager = GetComponent<TimeManager>();
        moviePlayer = GetComponent<MoviePlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        float time = timeManager.Timer;
        if (time > 2)
        {
            glowXsm.Play();
        }

        if (time > 4)
        {
            cigarettePart.Stop();
            glowSm.Play();
            smokeXsm.Play();
        }

        if (time > 6)
        {
            fireXsm.Play();
        }

        if (time > 8)
        {
            smokeSm.Play();
            fireSm.Play();
            fireSpark.Play();
            //继续播放
            moviePlayer.PlayFireMovie();
        }

        if (time > 10)
        {
            fireMd.Play();
            for (int i = 0; i < groundFires.Length; i++)
            {
                groundFires[i].SetActive(true);
            }
        }
    }
}
