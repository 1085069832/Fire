using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    float timer;
    public float Timer
    {
        get
        {
            return timer;
        }

        set
        {
            timer = value;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }
}
