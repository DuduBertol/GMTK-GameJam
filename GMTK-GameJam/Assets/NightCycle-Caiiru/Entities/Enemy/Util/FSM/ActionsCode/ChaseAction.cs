using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "FSM/Actions/Chase")]
public class ChaseAction : IAction
{
    public override void Act(StateMachine stateMachine)
    {
        Chase(stateMachine);
    }

    private void Chase(StateMachine stateMachine)
    {
        if (stateMachine.target == null) return;
        if (!stateMachine.CheckIfCountDownElapsed(stateMachine.stats.movementRate))
            return;
        stateMachine.agent.destination = stateMachine.target.position;
        stateMachine.agent.isStopped = false;
        if (stateMachine.agent.remainingDistance <= stateMachine.agent.stoppingDistance)
        {
            stateMachine.agent.isStopped = true;
        }
    }
}
