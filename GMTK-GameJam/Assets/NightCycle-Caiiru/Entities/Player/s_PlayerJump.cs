using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class s_PlayerJump : MonoBehaviour
{
    public bool jumpPressed; 
    public LayerMask _groundLayer;

    public bool isJumping; 
    public int jumpForce;
    public float groundStack;

    public float timeBetweenEachJump = 0.3f;
    private float timeForNextJump;

    private Rigidbody rigidbody;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time < timeForNextJump) return;
        if(!OnGround()) return;
        if(jumpPressed && !isJumping){
            rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); 
            timeForNextJump = Time.time + timeBetweenEachJump;
            isJumping = true;
        }
        if(isJumping && OnGround()){
            StartCoroutine("JumpToggle");
        }
    }

    IEnumerator JumpToggle(){
        yield return new WaitForSeconds(1f);
        isJumping = false;
    }
    public void OnJump(InputAction.CallbackContext context){
        jumpPressed = context.performed;
    }

    public bool OnGround(){ 
        Debug.DrawRay(transform.position - new Vector3(0,transform.localScale.y,0), Vector3.down * groundStack, Color.magenta); 
        return Physics.Raycast(transform.position - new Vector3(0,transform.localScale.y,0), Vector3.down * groundStack, 2f, _groundLayer);
    }
}
