using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_TentController : MonoBehaviour
{
    [SerializeField] private Transform eggAmountText;
    [SerializeField] private Transform eggPlacer;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Material dayMaterial;
    [SerializeField] private Material nightMaterial;

    private void Start() 
    {
        GameController.Instance.OnDayNightChanged += GameController_OnDayNightChanged;
    }

    private void GameController_OnDayNightChanged(object sender, EventArgs e)
    {
        if(GameController.Instance.GetIsDay())
        {
            meshRenderer.material = dayMaterial;
        }
        else
        {
            meshRenderer.material = nightMaterial;
        }
    }

    private void OnTriggerEnter(Collider collider) 
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            eggAmountText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            eggAmountText.gameObject.SetActive(false);
        }  
    }

    /* public void SetObjectAsChild(Transform objChild)
    {
        objChild.parent = eggPlacer;
        objChild.localPosition = Vector3.zero;
    } */
}
