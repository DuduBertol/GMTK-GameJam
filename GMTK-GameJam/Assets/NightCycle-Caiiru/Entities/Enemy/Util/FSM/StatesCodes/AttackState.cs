using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "FSM/States/attackState", fileName = "New Attack State")]
public class AttackState : State
{
    [Tooltip("In Seconds"), Range(0.3f,5f)]public float cooldownBetweenAttacks;
    [Range(0.5f, 2f)] public float attackRange;

    public Transform target;
    public LayerMask attackMask;
    private NavMeshAgent _agent;
    public override void OnEnter(StateMachine statemachine)
    {
        base.OnEnter(statemachine);
        _agent = statemachine.GetAgent();
        _agent.isStopped = true; 
    }

    public override void OnUpdate(StateMachine statemachine)
    {
        base.OnUpdate(statemachine);
        if (Physics.Raycast(_agent.transform.position, _agent.transform.forward, out RaycastHit _hit,
                attackRange,attackMask))
        {
            if (_hit.transform.GetComponent<Entity>())
            {
                Debug.Log("Hiting an entity");
            }
        }
    }

    public override void OnExit(StateMachine statemachine)
    {
        base.OnExit(statemachine);
    }
}
