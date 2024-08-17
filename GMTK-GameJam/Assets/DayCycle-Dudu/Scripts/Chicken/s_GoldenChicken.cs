using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_GoldenChicken : MonoBehaviour
{
    [SerializeField] private Transform eggSpawner;
    [SerializeField] private Transform goldenEggPrefab;
    [SerializeField] private Vector3 goldenEggLastScale;
    [SerializeField] private float scaleAmountToIncrease;

    private void Start() 
    {
        GameController.Instance.OnDayNightChanged += GameController_OnDayNightChanged;
    }

    private void GameController_OnDayNightChanged(object sender, EventArgs e)
    {
        if(GameController.Instance.GetIsDay())
        {
            if(eggSpawner.childCount == 0) //Spawner de ovos não possui filho, logo posso spawnar outro
            {
                Transform eggTransform = Instantiate(goldenEggPrefab, eggSpawner);
                eggTransform.localPosition = Vector3.up * 3;
            }
            else
            {
                Debug.Log("Não foi possível spawnar o ovo. Já existe um nessa posição.");
            }
        }

        goldenEggLastScale += new Vector3(scaleAmountToIncrease, scaleAmountToIncrease, scaleAmountToIncrease);
        goldenEggPrefab.transform.localScale = goldenEggLastScale;
    }
}
