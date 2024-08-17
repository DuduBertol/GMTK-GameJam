using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_PlayerCollision : MonoBehaviour
{
    public Transform holdPos;
    public Transform objChild;
    [SerializeField] private Transform eggPlacer; 

    private void Start() 
    {
        GameInput.Instance.OnDropAction += GameInput_OnDropAction;
    }

    private void GameInput_OnDropAction(object sender, EventArgs e)
    {
        objChild.parent = null;
        objChild.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        objChild = null;
    }


    private void OnCollisionEnter(Collision collisor) 
    {
        if(collisor.gameObject.CompareTag("GoldenEgg")) //colisão dos ovos de ouro por tag
        {
            if(objChild == null)
            {
                objChild = collisor.transform;
                collisor.transform.parent = holdPos;
                collisor.transform.localPosition = Vector3.zero;
                collisor.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                Debug.Log("Colidiu com a Galinha.");
            }
        }
    }

    private void OnTriggerEnter(Collider collider) 
    {
        if(collider.gameObject.CompareTag("Tent")) // colisão da tenda por tag
        {
            Debug.Log("Colidiu com a Tenda.");

        }
        if(collider.gameObject.CompareTag("EggPlacer")) // colisão da tenda por tag
        {
            Debug.Log("Colidiu com o Egg Placer da Tenda.");
            
            if(objChild != null)
            {
                objChild.parent = collider.transform;

                float multiplier = UnityEngine.Random.Range(0, 1.5f);
                objChild.localPosition = Vector3.one*multiplier;
                objChild.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                objChild = null;
                // objChild.parent = collider.transform.GetChild(0);
                
                GameController.Instance.IncreaseEggAmount(1);
            }
        }
    }
}
