using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "FSM/States/ChaseState", fileName = "New Chase State")]
public class ChaseState : State
{
    public float movementSpeed = 3.5f;
    public float stoppingDistance = 2f;

    public NavMeshAgent _agent;
    public override void OnEnter(StateMachine statemachine)
    {
        _agent = statemachine.GetAgent();
        Debug.Log("Enter of Chase");
        if(_agent == null)
            _agent = statemachine.transform.GetComponent<NavMeshAgent>();
    }

    public override void OnUpdate(StateMachine statemachine)
    {
        base.OnUpdate(statemachine);
        if (statemachine.target == null)
        {
            Debug.LogWarning("No target set for chaseState");
            return;
        }

        if (_agent == null || !_agent.isOnNavMesh)
        {
            Debug.LogWarning("Agent is not on navmesh or agent is null");
            return; 
        }
        
        /*
        statemachine.SetAgentStoped(false, movementSpeed); 
        statemachine.SetStoppingDistance(stoppingDistance);
        statemachine.SetDestination(statemachine.target);
        */
        _agent.isStopped = false;
        _agent.speed = movementSpeed;
        _agent.stoppingDistance = stoppingDistance;
        _agent.SetDestination(statemachine.target.position);


    }

    public override void OnExit(StateMachine statemachine)
    {
        if (_agent != null)
        {
            _agent.isStopped = true;
        }   
    }
}
