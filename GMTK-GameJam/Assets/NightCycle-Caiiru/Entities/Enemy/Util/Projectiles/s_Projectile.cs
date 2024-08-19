using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_Projectile : MonoBehaviour
{
    private int projectileDamage;
    public int projectileSpeed;

    private Vector3 _direction;
    private bool _isActive;
    
    //Components
    private Rigidbody _rigidbody;
    private Collider _collider;
    void OnEnable()
    {
        _collider = GetComponent<Collider>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Create(Vector3 direction, int _damage)
    {
        this._direction = direction;
        this.projectileDamage = _damage;
        _isActive = true;
        /*
        _rigidbody.useGravity = false;
        this.projectileDamage = _damage;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.AddForce(direction.normalized * projectileSpeed, ForceMode.Impulse);*/
    }
    void FixedUpdate(){
        if(!_isActive)
            return;

        transform.position += _direction * (projectileSpeed * Time.fixedDeltaTime);
    }
    
    public void OnTriggerEnter(UnityEngine.Collider other)
    { 
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.GetComponent<Entity>().TakeDamage(projectileDamage);
           
        } 
        _isActive=false;
        this.gameObject.SetActive(false);
    } 
}
