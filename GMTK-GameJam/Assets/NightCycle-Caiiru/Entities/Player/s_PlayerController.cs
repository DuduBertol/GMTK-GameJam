using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class s_PlayerController : MonoBehaviour
{
    [SerializeField] private Vector3 _movementInput;
    [SerializeField] private float _movementSpeed = 7f;

    private Vector3 _readInput = Vector3.zero;
    
    #region Components
    private Rigidbody _rigidbody;
    private Collider _collider;
    #endregion
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _readInput = context.ReadValue<Vector2>();
        _movementInput = new Vector3(_readInput.x, _rigidbody.velocity.y, _readInput.y);
    }
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (_movementInput != Vector3.zero)
        {
            //_rigidbody.AddForce(_movementInput * (_movementSpeed * Time.fixedDeltaTime));
            //_rigidbody.velocity = (_movementInput * (_movementSpeed * Time.fixedDeltaTime));
            _rigidbody.MovePosition(_rigidbody.position + _movementInput.normalized * (Time.deltaTime * _movementSpeed));
        }

        if(_movementInput.x < 0)
        {
            LeanTween.rotateY(gameObject, 180, 0.25f);
            // gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
        else if (_movementInput.x > 0)
        {
            LeanTween.rotateY(gameObject, 0, 0.25f);
            // gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
    }
 
}
