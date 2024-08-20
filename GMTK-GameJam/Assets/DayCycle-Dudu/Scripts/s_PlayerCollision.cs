using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if(objChild != null)
        {
            ClearPlayerObjectChild();
            GameController.Instance.hasGoldenChicken = false;
            SoundManager.Instance.PlayDropGetItemSound(Camera.main.transform.position, 1f);
        }
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

                SoundManager.Instance.PlaySellEggSound(Camera.main.transform.position, 1f);
            }
        }

        if(collider.gameObject.CompareTag("BeanTree"))
        {
            overlappingBeanTree = true;
            collider.transform.parent.parent.GetComponent<s_BeanTreeController>().ToggleLeanText(true);
        }

        if(collider.gameObject.CompareTag("WinGame"))
        {
            if(GameController.Instance.hasGoldenChicken)
            {
                Debug.Log("You Win!");
                SceneManager.LoadScene("EndScene");
            }
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
            collider.transform.parent.parent.GetComponent<s_BeanTreeController>().ToggleLeanText(false);
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
