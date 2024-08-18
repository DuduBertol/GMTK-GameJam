using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class s_TentController : MonoBehaviour
{
    [Header("Tent")]
    [SerializeField] private Transform spawnTradePlacer;
    
    [Header("Egg Amount")]
    [SerializeField] private Transform eggAmountTextTransform;
    [SerializeField] private TextMeshProUGUI eggAmountText;

    [Header("Fertilizer")]
    [SerializeField] private int fertilizerPrice;
    [SerializeField] private Transform fertilizerPrefab;
    
    [Header("Others")]
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Material dayMaterial;
    [SerializeField] private Material nightMaterial;
    private Transform playerTransform;

    private void Start() 
    {
        GameController.Instance.OnDayNightChanged += GameController_OnDayNightChanged;
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if(playerTransform.gameObject.GetComponent<s_PlayerCollision>().overlappingTent)
        {
            if(GameController.Instance.eggAmount >= fertilizerPrice)
            {
                Debug.Log("Você gastou ovos.");
                GameController.Instance.eggAmount -= fertilizerPrice;
                
                Transform fertilizer = Instantiate(fertilizerPrefab, spawnTradePlacer);
                UpdateTextVisual();
            };
        }
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
            playerTransform = collider.transform;

            eggAmountText.gameObject.SetActive(true);
            UpdateTextVisual();
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

    public void UpdateTextVisual()
    {
        eggAmountText.text = "Egg Amount: " + GameController.Instance.eggAmount.ToString();
    }
}
