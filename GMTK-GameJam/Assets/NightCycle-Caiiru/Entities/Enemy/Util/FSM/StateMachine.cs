using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateMachine : MonoBehaviour
{
    [Header("Entity Stats")] public s_EnemyStats stats;
    
    [Space(10)]
    [Header("State Machine Settings")]
    public State currentState;
    public State firstState;
    public State remainInState;
    public Transform target;
    public s_EnemyAttackManager attackManager;
    
    
    [Space(5)]
    public bool aiActive;

    [Header("Debug")]
    public bool drawSphere;
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public float stateTimeElapsed;

    void Awake()
    {
        //target = GameController.Instance.GetPlayer().transform;// TIRAR ISSO DEPOIS
        agent = transform.GetComponent<NavMeshAgent>();
        attackManager = transform.GetComponent<s_EnemyAttackManager>();
    }

    void Start()
    {
        agent.enabled = aiActive;
    }

    // Update is called once per frame
    void Update()
    {
        if (!aiActive)
            return;
        currentState.UpdateState(this);
        agent.stoppingDistance = stats.stoppingDistance;
        agent.speed = stats.movementSpeed;
        
    }
    public void StartStateMachine(s_EnemyStats stats){
        this.stats = stats;
        if(!this.transform.GetComponent<EnemyEntity>()){
            return;
        }
        this.transform.GetComponent<EnemyEntity>().Create();
        if(agent == null) agent = transform.GetComponent<NavMeshAgent>();
        //currentState = firstState;
        TransitionToState(firstState);
        agent.enabled = aiActive; 
        agent.isStopped = !aiActive;
        agent.radius = 0.5f;

    }
    public void EndStateMachine(){
        agent.isStopped = true;
        agent.enabled = false;
    }

    public void TransitionToState(State nextState)
    {
        if (nextState != remainInState)
        {

            OnExitState(currentState);
            currentState = nextState;
            OnEnterState(currentState);
        }
    }

    public bool CheckIfCountDownElapsed(float duration)
    {
        stateTimeElapsed += Time.deltaTime;
        return (stateTimeElapsed >= duration);
    }
    private void OnEnterState(State state){
        state.EnterState(this);
    }
    private void OnExitState(State state)
    {
        stateTimeElapsed = 0;
    }

    private void OnDrawGizmosSelected()
    {
         if(drawSphere){
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(agent.transform.position, stats.visionRange);
         }
    }
}
