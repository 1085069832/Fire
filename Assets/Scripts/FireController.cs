using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{

    [SerializeField] GameObject cigarette;

    /// <summary>
    /// 开始
    /// </summary>
    public void OnStartFire()
    {
        FirePartController[] firePartControllers = cigarette.GetComponentsInChildren<FirePartController>();
        for (int i = 0; i < firePartControllers.Length; i++)
        {
            firePartControllers[i].StartFire();
        }
    }
    /// <summary>
    /// 重置火
    /// </summary>
    public void OnStopFire()
    {
        FirePartController[] firePartControllers = cigarette.GetComponentsInChildren<FirePartController>();
        for (int i = 0; i < firePartControllers.Length; i++)
        {
            firePartControllers[i].InitFire();
        }
    }
}
