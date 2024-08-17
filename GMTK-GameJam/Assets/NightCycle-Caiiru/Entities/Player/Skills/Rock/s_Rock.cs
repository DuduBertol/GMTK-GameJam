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
    #region Components

    private Rigidbody _rigidbody;

    #endregion
    void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody>(); 
    }

    public void Create(Vector3 _force, int _damage)
    {
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
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!_hitSomething) _hitSomething=true;
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.transform.GetComponent<Entity>().TakeDamage(_rockDamage);
        }
    }
}
