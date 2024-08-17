using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public bool isWalking;
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
    }

    private void FixedUpdate() 
    {
        HandleMovement(); // Desconsidera essa movimentação, ta toda quebrada
    }


    private void HandleMovement()
    {
        
        Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalizedPlayer();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = .7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);
        
        if (!canMove)
        {
            //Cannot move towards moveDir
            
            //Attempt only X movement
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = (moveDir.x < -.5f || moveDir.x > +.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove)
            {
                //Can move only on the X
                moveDir = moveDirX;
            }
            else
            {
                //Cannot move only on the X

                //Attemp only Z movement
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove =  (moveDir.z < -.5f || moveDir.z > +.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if (canMove)
                {
                    //Can move only on the Z
                    moveDir = moveDirZ;
                }
                else
                {
                    //Cannot move in any direction
                }
            }
        }

        if(canMove)
        {
            transform.position += moveDir * moveDistance;
        }

        isWalking = moveDir != Vector3.zero;
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
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
    }
}
