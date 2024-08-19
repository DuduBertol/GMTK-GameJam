using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
    public static GameController Instance {get; private set;}

    public event EventHandler OnDayNightChanged;

    [Header("General")]
    [SerializeField] private float runningTime;
    [SerializeField] private float dayTime;
    [SerializeField] private float nightTime;
    [SerializeField] private bool isDay;

    [Header("Eggs")]
    public int eggAmount;
    
    [Header("Bean Tree")]
    [SerializeField] private Transform beanTree;
    [SerializeField] private float scaleMultiplier;
    [SerializeField] private float beanXZScaleMax;

    [Header("Player")]
    public GameObject player;


    [Header("Lightning")]
    [SerializeField] private Transform direcLightDay;
    [SerializeField] private Transform direcLightNight;
    [SerializeField] private Vector3 direcLightFiftyRotation;
    [SerializeField] private Vector3 direcLightZeroRotation;

    private void Awake() 
    {
        if(Instance != this && Instance != null){
            Destroy(this.gameObject);
        }

        Instance = this;    
         if(player == null){
            player = GameObject.Find("PlayerPrefab");
        }
    }
    public void Start(){
       
    }

    private void Update() 
    {
        PassingTime();
    }

    private void PassingTime() // Sistema de dia e noite - variável com os valores colocados
    {
        runningTime += Time.deltaTime;

        if(isDay)
        {
            direcLightDay.gameObject.SetActive(true);
            direcLightDay.rotation = Quaternion.Lerp(direcLightDay.rotation, Quaternion.Euler(direcLightFiftyRotation), 0.1f);
            
            direcLightNight.rotation = Quaternion.Lerp(direcLightNight.rotation, Quaternion.Euler(direcLightZeroRotation), 0.1f);
            direcLightNight.gameObject.SetActive(false);

            if(runningTime > dayTime)
            {
                isDay = false;
                runningTime = 0f;
                OnDayNightChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        else
        {
            direcLightDay.gameObject.SetActive(false);
            direcLightDay.rotation = Quaternion.Lerp(direcLightDay.rotation, Quaternion.Euler(direcLightZeroRotation), 0.1f);

            direcLightNight.rotation = Quaternion.Lerp(direcLightNight.rotation, Quaternion.Euler(direcLightFiftyRotation), 0.1f);
            direcLightNight.gameObject.SetActive(true);
            
            if(runningTime > nightTime)
            {
                isDay = true;
                runningTime = 0f;
                OnDayNightChanged?.Invoke(this, EventArgs.Empty);
            }
        }

    }

    public bool GetIsDay()
    {
        return isDay;
    }
    /* public int GetEggAmount()
    {
        return eggAmount;
    }
    public void SetEggAmount(int value)
    {
        eggAmount = value;
    } */

    public void IncreaseBeanTree()
    {
        beanTree.gameObject.GetComponent<s_BeanTreeController>().UpdateBeanTreeVisual();
    }

    public float GetTimeReason()
    {
        return runningTime / dayTime;
    }
    public GameObject GetPlayer(){
        return player;
    }
}

