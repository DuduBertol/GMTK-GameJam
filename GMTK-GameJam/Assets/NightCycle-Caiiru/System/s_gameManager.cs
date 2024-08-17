using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_gameManager : MonoBehaviour
{
    private GameObject playerGO;

    public static s_gameManager Instance { get; private set;}
    
    private void Awake()
    {
        if (Instance != this && Instance != null)
        {
            Destroy(this.gameObject);
            
        }

        Instance = this;
        playerGO = GameObject.Find("Player");
    }

    void Start()
    {
        if(playerGO == null)
            Debug.LogError("Player not finded");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetPlayerObject()
    {
        return playerGO;
    }
}
