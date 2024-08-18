using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(s_enemyController))]
public class s_enemyMelee : MonoBehaviour
{
    [Header("Attack Range Settings")] 
    [SerializeField,Range(0.5f,1.25f)]
    private float _attackRange;
    
    
    //COMPONENTS
    private s_enemyController _controller;
    private NavMeshAgent _agent;
    
    [Space(10)]
    [Header("Debug")] 
    [SerializeField]private bool _debugButton = false;
 
    void Start()
    {
        _controller = GetComponent<s_enemyController>();
        _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(_agent.transform.position + _agent.transform.forward / 2, _agent.transform.forward, out RaycastHit hit,
                _attackRange))
        {
            if(_debugButton)
                Debug.Log(hit.transform.name);
        }

        
        Debug.DrawRay(_agent.transform.position + _agent.transform.forward / 2, _agent.transform.forward * _attackRange,Color.green);
        
        
        //Debug.DrawRay(transform.position, _agent.transform.forward * _attackRange, Color.magenta);
        //FaceTarget(_controller.GetTarget().position);
    }

    void FaceTarget(Vector3 _destination)
    {
        Vector3 lookPos = _destination - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1f);
    }

    private void OnDrawGizmos()
    {
        if (!_debugButton)
            return;
        if(_agent != null)
            Gizmos.DrawWireSphere(_agent.transform.position, _attackRange);
    }
}
