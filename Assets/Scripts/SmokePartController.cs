using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 控制烟
/// </summary>
public class SmokePartController : MonoBehaviour
{
    new ParticleSystem particleSystem;
    ParticleSystem.EmissionModule pEm;//强度
    ParticleSystem.ShapeModule pSm;//shape
    ParticleSystem.MinMaxCurve pMc;
    ParticleSystem.MainModule pMm;
    ParticleSystem.MinMaxCurve pMmPMc;
    FirePartController firePartController;
    float boxX;
    float boxY;
    float boxZ;
    float fireDiffuseSpeed;
    float time;
    Vector3 smokeBoxDefault;
    [SerializeField] float delayTime = 1;//产生火后产生烟的延迟时间
    [SerializeField] float smokeMinHigh = 2.5f;//最低高度
    [SerializeField] float smokeMaxHigh = 2.5f;//最高高度

    // Use this for initialization
    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        firePartController = GetComponentInParent<FirePartController>();

        InitSmokeSize();

        InitFireHigh();

        RecordFireData();
    }

    /// <summary>
    /// 记录烟初始数据
    /// </summary>
    private void RecordFireData()
    {
        smokeBoxDefault = pSm.box;
    }

    // Update is called once per frame
    void Update()
    {
        if (firePartController.isFire)
        {
            time += Time.deltaTime;
            if (time > delayTime)
            {
                pMc.constant += Time.deltaTime * fireDiffuseSpeed * 5;
                pEm.rateOverTime = pMc;

                //烟大小
                boxX = pSm.box.x + Time.deltaTime * fireDiffuseSpeed * 0.1f;
                boxY = pSm.box.y + Time.deltaTime * fireDiffuseSpeed * 0.1f;
                boxZ = pSm.box.z + Time.deltaTime * fireDiffuseSpeed * 0.1f;

                boxX = Mathf.Clamp(boxX, 0, 2.8f);
                boxY = Mathf.Clamp(boxY, 0, 2.8f);
                boxZ = Mathf.Clamp(boxZ, 0, 0.3f);

                pSm.box = new Vector3(boxX, boxY, boxZ);

                //高度
                pMmPMc.constantMax += Time.deltaTime * fireDiffuseSpeed * 0.05f;
                if (pMmPMc.constantMax > smokeMaxHigh)
                {
                    pMmPMc.constantMax = smokeMaxHigh;
                }
                pMm.startLifetime = pMmPMc;
            }
        }
    }

    /// <summary>
    /// 烟强度和大小
    /// </summary>
    private void InitSmokeSize()
    {
        fireDiffuseSpeed = firePartController.fireDiffuseSpeed;
        pMc = new ParticleSystem.MinMaxCurve();
        pEm = particleSystem.emission;
        pSm = particleSystem.shape;
    }

    /// <summary>
    /// 烟高度
    /// </summary>
    private void InitFireHigh()
    {
        pMm = particleSystem.main;
        pMmPMc = new ParticleSystem.MinMaxCurve(smokeMinHigh, smokeMinHigh);
    }
    /// <summary>
    /// 初始化烟
    /// </summary>
    public void InitSmoke()
    {
        time = 0;
        pMc.constant = 0;
        pEm.rateOverTime = pMc;
        pSm.box = smokeBoxDefault;
        pMmPMc.constantMax = smokeMaxHigh;
        pMm.startLifetime = pMmPMc;
    }
}
