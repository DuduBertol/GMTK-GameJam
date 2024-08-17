using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_PlayerCollision : MonoBehaviour
{
    public Transform holdPos;
    public Transform objChild;

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
        if(collisor.gameObject.CompareTag("Tent")) // colisão da tenda por tag
        {
            Debug.Log("Colidiu com a Tenda.");
        }
        if(collisor.gameObject.CompareTag("EggPlacer")) // colisão da tenda por tag
        {
            Debug.Log("Colidiu com o Egg Placer da Tenda.");
            
            objChild.parent = collisor.transform.GetChild(0);
            objChild.transform.localPosition = Vector3.zero;
        }
    }
}
