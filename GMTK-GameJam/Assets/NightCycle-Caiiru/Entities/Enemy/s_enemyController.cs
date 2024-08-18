using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(Rigidbody))] 
public class s_enemyController : MonoBehaviour
{ 

    [SerializeField] private float _movementSpeed = 1f;
    [SerializeField] private Transform targetTransform;

    #region Components

    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Collider _collider;
    [SerializeField] private NavMeshAgent _agent;

    [Space(10)] [Header("Debug")] public bool debugButton;

    #endregion
    void OnEnable()
    { 
        //_agent = GetComponent<NavMeshAgent>();
        //_agent = GetComponent<NavMeshAgent>();
        //_agent.enabled = false;
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponentInChildren<Collider>();
        //_agent.speed = _movementSpeed;
        
    }
    

    // Update is called once per frame
    void Update()
    {
        if (debugButton)
        {
            debugButton = false;
            //SetDestination(targetTransform); 
        }
    }

     

    public void SetDestination(Transform _transform)
    {
        _agent.enabled = true;
        targetTransform = _transform;
    }

    public Transform GetTarget()
    {
        return targetTransform;
    }

    void FixedUpdate()
    {
        if(targetTransform != null)
            _agent.SetDestination(targetTransform.position);
    }
}
