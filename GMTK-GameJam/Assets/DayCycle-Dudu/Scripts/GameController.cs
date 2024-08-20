using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance {get; private set;}

    public event EventHandler OnDayNightChanged;

    [Header("General")]
    public bool isPaused;
    [SerializeField] private float runningTime;
    [SerializeField] private float dayTime;
    [SerializeField] private float nightTime;
    [SerializeField] private bool isDay;

    [SerializeField] private int dayCount;

    [Header("Canvas")]
    [SerializeField] private s_CanvasController canvasController;

    [Header("Golden Chicken")]
    public int eggAmount;
    public bool hasGoldenChicken;
    [SerializeField] private GameObject goldenChicken;
    
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
    private void Start()
    {
        DisplayCanvasText("Sell golden eggs and fertilize your Bean tree!");

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
                OnDayNightChanged?.Invoke(this, EventArgs.Empty); // Night Become
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
                OnDayNightChanged?.Invoke(this, EventArgs.Empty); // Day Become
                dayCount++;
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
            
        if(beanTree.gameObject.GetComponent<s_BeanTreeController>().treeLevel < beanTree.gameObject.GetComponent<s_BeanTreeController>().baseAmounts)
        {
            beanTree.gameObject.GetComponent<s_BeanTreeController>().UpdateBeanTreeVisual();
        }
        
        if(beanTree.gameObject.GetComponent<s_BeanTreeController>().treeLevel >= beanTree.gameObject.GetComponent<s_BeanTreeController>().baseAmounts)
        {
            DisplayCanvasText("Your tree achievs the sky! \n Go home with your golden chicken!");

            hasGoldenChicken = true;

            goldenChicken.GetComponent<BoxCollider>().enabled = true;
            goldenChicken.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    public float GetTimeReason()
    {
        return runningTime / dayTime;
    }
    public GameObject GetPlayer(){
        return player;
    }
    public int GetDayNumber(){
        return dayCount;
    }

    public void DisplayCanvasText(string text)
    {
        canvasController.DisplayText(text);
    }
}

