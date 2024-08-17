using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(Rigidbody))] 
public class s_enemyController : MonoBehaviour
{
    private NavMeshAgent _agent;

    [SerializeField] private float _movementSpeed = 1f;
    [SerializeField] private Transform targetTransform;

    #region Components

    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Collider _collider;

    #endregion
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponentInChildren<Collider>();
        _agent.speed = _movementSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        _agent.SetDestination(targetTransform.position);
    }
}
