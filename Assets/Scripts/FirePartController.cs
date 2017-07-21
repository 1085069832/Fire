using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 控制火
/// </summary>
public class FirePartController : MonoBehaviour
{
    new ParticleSystem particleSystem;
    ParticleSystem.MinMaxCurve pMc;
    ParticleSystem.EmissionModule pEm;//火强度
    ParticleSystem.ShapeModule pSm;//shape
    ParticleSystem.MainModule pMm;
    ParticleSystem.MinMaxCurve pMmPMc;
    [HideInInspector] public bool isFire;
    float time = 0;
    bool startFire;//开始
    [SerializeField] float fireStartTime = 2f;//开始生成火的时间
    [SerializeField] float fireDiffuseEndTime = 5f;//停止时间
    public float fireDiffuseSpeed = 2f;//扩散速度
    [SerializeField] float fireMinHigh = 3f;//最低高度
    [SerializeField] float fireMaxHigh = 5f;//最高高度

    // Use this for initialization
    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();

        InitFireSize();

        InitFireHigh();

        RecordFireData();
    }

    /// <summary>
    /// 记录火初始数据
    /// </summary>
    private void RecordFireData()
    {
        PlayerPrefs.SetFloat(MyConst.FIREANGEL, pSm.angle);
        PlayerPrefs.SetFloat(MyConst.FIRERADIUS, pSm.radius);
    }

    // Update is called once per frame
    void Update()
    {
        if (startFire)
        {
            time += Time.deltaTime;
            if (time > fireStartTime)
            {
                isFire = true;
                if (time > fireDiffuseEndTime + fireStartTime)
                {
                    isFire = false;
                    return;
                }
                //火强度和扩散
                pMc.constant += Time.deltaTime * fireDiffuseSpeed * 10;
                pEm.rateOverTime = pMc;
                pSm.angle += Time.deltaTime * fireDiffuseSpeed;
                pSm.radius += Time.deltaTime * 0.1f * fireDiffuseSpeed;
                //高度
                pMmPMc.constantMax += Time.deltaTime * fireDiffuseSpeed * 0.1f;
                if (pMmPMc.constantMax > fireMaxHigh)
                {
                    pMmPMc.constantMax = fireMaxHigh;
                }
                pMm.startLifetime = pMmPMc;
            }
        }
    }

    /// <summary>
    /// 火强度和大小
    /// </summary>
    private void InitFireSize()
    {
        pMc = new ParticleSystem.MinMaxCurve();
        pEm = particleSystem.emission;
        pSm = particleSystem.shape;
    }

    /// <summary>
    /// 火高度
    /// </summary>
    private void InitFireHigh()
    {
        pMm = particleSystem.main;
        pMmPMc = new ParticleSystem.MinMaxCurve(fireMinHigh, fireMinHigh);
    }

    /// <summary>
    /// 初始化火
    /// </summary>
    public void InitFire()
    {
        startFire = true;
        isFire = false;
        time = 0;
        pMc.constant = 0;
        pEm.rateOverTime = pMc;
        pMmPMc.constantMax = fireMaxHigh;
        pMm.startLifetime = pMmPMc;
        pSm.angle = PlayerPrefs.GetFloat(MyConst.FIREANGEL);
        pSm.radius = PlayerPrefs.GetFloat(MyConst.FIRERADIUS);
        GetComponentInChildren<SmokePartController>().InitSmoke();
        particleSystem.Stop();
        particleSystem.Clear();
        particleSystem.Play();
    }
}
