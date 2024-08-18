using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class s_PlayerCollision : MonoBehaviour
{
    public Transform holdPos;
    public Transform objChild;
    public bool overlappingTent;
    public bool overlappingBeanTree;

    private Transform tentTransform;

    // [SerializeField] private Transform eggPlacer; 

    private void Start() 
    {
        GameInput.Instance.OnDropAction += GameInput_OnDropAction;
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if(overlappingBeanTree)
        {
            if(objChild != null && objChild.GetComponent<s_GettableObject>().objectType == s_GettableObject.ObjectType.Fertilizer)
            {
                GameController.Instance.IncreaseBeanTree();
                
                Transform transformChild = objChild;
                ClearPlayerObjectChild();
                Destroy(transformChild.gameObject);
            }
        }
    }

    private void GameInput_OnDropAction(object sender, EventArgs e)
    {
        ClearPlayerObjectChild();
    }



    private void OnTriggerEnter(Collider collider) 
    {
        if(collider.gameObject.CompareTag("Tent"))
        {
            tentTransform = collider.transform;

            overlappingTent = true;
            Debug.Log("Colidiu com a Tenda.");
        }

        if(collider.gameObject.CompareTag("EggPlacer"))
        {
            Debug.Log("Colidiu com o Egg Placer da Tenda.");
            
            if(objChild != null && objChild.gameObject.GetComponent<s_GettableObject>().objectType == s_GettableObject.ObjectType.GoldenEgg)
            {
                objChild.parent = collider.transform;
                objChild.gameObject.GetComponent<Rigidbody>().isKinematic = false;

                float multiplier = UnityEngine.Random.Range(0, 1.5f);
                objChild.localPosition = Vector3.one*multiplier;
                
                objChild = null;
                
                GameController.Instance.eggAmount++;
                tentTransform.gameObject.GetComponent<s_TentController>().UpdateTextVisual();
            }
        }

        if(collider.gameObject.CompareTag("BeanTree"))
        {
            overlappingBeanTree = true;
        }
    }

    private void OnTriggerExit(Collider collider) 
    {
        if(collider.gameObject.CompareTag("Tent"))
        {
            overlappingTent = false;
        }
        if(collider.gameObject.CompareTag("BeanTree"))
        {
            overlappingBeanTree = false;
        }
    }

    public void SetPlayerObjectChild(Transform transformChild)
    {
        if(objChild != null) return;

        objChild = transformChild;
                
        objChild.parent = holdPos;
        objChild.localPosition = Vector3.zero;
        objChild.gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }
    public void ClearPlayerObjectChild()
    {
        objChild.gameObject.GetComponent<Rigidbody>().isKinematic = false;

        objChild.parent = null;
        objChild = null;
    }
}
