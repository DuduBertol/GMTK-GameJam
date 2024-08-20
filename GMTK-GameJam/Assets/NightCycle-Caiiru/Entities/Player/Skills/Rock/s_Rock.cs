using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class s_Rock : MonoBehaviour
{
    [SerializeField] private int _rockDamage = 1;

    private Vector3 _angleVelocity;
    private bool _hitSomething = false;

    [Header("Rock Despawn Settings")] 
    [SerializeField]
    private float _lifeDuration = 1.5f;

    [SerializeField] 
    private float _negativeMoveY = 15f;
    
    [SerializeField]
    private float _animationDuration = 0.3f;
    #region Components

    private LTDescr _tweenAnimation;
    [SerializeField]private Rigidbody _rigidbody;

    private bool _wasThrow = false;

    #endregion
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>(); 
    }

    public void Create(Vector3 _force, int _damage)
    {
        if(this._wasThrow == true) return;
        this._wasThrow = true;
        _rigidbody.velocity = Vector3.zero;
        _hitSomething = false;
        this._rockDamage = _damage;
        _rigidbody.AddForce(_force, ForceMode.Impulse);
        _angleVelocity = new Vector3(Random.Range(-360f, 360f), Random.Range(-360f, 360f), Random.Range(-360f, 360f));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!_hitSomething)
        {
            Quaternion deltaRotation = Quaternion.Euler(_angleVelocity * Time.fixedDeltaTime);
            _rigidbody.MoveRotation(_rigidbody.rotation * deltaRotation);
            
        }
        else{ 
            StartCoroutine("RockDespawn");
        }
    }

    IEnumerator RockDespawn()
    {
        
        yield return new WaitForSeconds(_lifeDuration);
        _tweenAnimation = transform.gameObject.LeanMoveY(transform.position.y - _negativeMoveY, _animationDuration);

        yield return new WaitForSeconds(_animationDuration);
        LeanTween.cancel(_tweenAnimation.id); 
        this._wasThrow = false;
        this.gameObject.SetActive(false);
    }
    /*
    private void OnCollisionEnter(Collision other)
    {
        if (!_hitSomething) _hitSomething=true;
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.transform.GetComponent<Entity>().TakeDamage(_rockDamage);
        }
    }   
    */
    private void OnTriggerEnter(Collider collider){
         if (!_hitSomething) _hitSomething=true;
        if (collider.gameObject.CompareTag("Enemy"))
        {
            collider.transform.GetComponent<Entity>().TakeDamage(_rockDamage);
        }
    }
}
