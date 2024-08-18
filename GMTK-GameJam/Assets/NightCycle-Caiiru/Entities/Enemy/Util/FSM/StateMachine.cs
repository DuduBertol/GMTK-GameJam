using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateMachine : MonoBehaviour
{
    public State currentState; 
    public Transform target;

    [SerializeField] private NavMeshAgent agent;
    void Start()
    {
        Debug.Log($"{transform.name} started his statemachine");
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player").transform;
        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == null)
            return;
        
        currentState.OnUpdate(this);
        /*
        foreach (var transition in transitions)
        {
            if (transition.ShouldTransition(this))
            {
                SwitchState(transition.targetState);
                break;
            }
        }
        */
    }

    public void SwitchState(State newState)
    {
        Debug.Log($"Changing from  {currentState} to {newState}");
        if(currentState!=null)
            currentState.OnExit(this);

        currentState = newState;
        
        if(currentState!= null)
            currentState.OnEnter(this);

 
    }

    public NavMeshAgent GetAgent()
    {
        return agent;
    }

    public void SetAgentStoped(bool value)
    {
        agent.isStopped = value;
    } public void SetAgentStoped(bool value, float _speed)
    {
        agent.isStopped = value;
        agent.speed = _speed;
    }

    public void SetStoppingDistance(float _distance)
    {
        agent.stoppingDistance = _distance;
    }

    public void SetDestination(Transform _target)
    {
        agent.SetDestination(_target.position);
    }
    
}
