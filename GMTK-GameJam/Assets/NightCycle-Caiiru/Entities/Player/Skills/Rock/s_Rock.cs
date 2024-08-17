using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_Rock : MonoBehaviour
{
    #region Components

    private Rigidbody _rigidbody;

    #endregion
    void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawRay(transform.position, transform.position + _rigidbody.velocity.normalized, Color.yellow);
    }
}
