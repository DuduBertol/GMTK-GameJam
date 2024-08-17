using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
    public static GameController Instance {get; private set;}

    public event EventHandler OnDayNightChanged;

    [SerializeField] private float runningTime;
    [SerializeField] private float dayTime;
    [SerializeField] private float nightTime;
    [SerializeField] private bool isDay;

    [SerializeField] private Transform direcLight;
    [SerializeField] private Vector3 direcLightDayRotation;
    [SerializeField] private Vector3 direcLightNightRotation;

    private void Awake() 
    {
        Instance = this;    
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
            direcLight.rotation = Quaternion.Lerp(direcLight.rotation, Quaternion.Euler(direcLightDayRotation), 0.1f);

            if(runningTime > dayTime)
            {
                isDay = false;
                runningTime = 0f;
                OnDayNightChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        else
        {
            direcLight.rotation = Quaternion.Lerp(direcLight.rotation, Quaternion.Euler(direcLightNightRotation), 0.1f);
            
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
}

