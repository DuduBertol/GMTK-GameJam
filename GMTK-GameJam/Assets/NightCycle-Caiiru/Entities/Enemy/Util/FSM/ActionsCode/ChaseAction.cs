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
        var _offset = Random.Range(-2, 2);
        stateMachine.agent.destination =  (stateMachine.target.position + new Vector3(_offset, 0, 0));
        stateMachine.agent.isStopped = false;
        if (stateMachine.agent.remainingDistance < stateMachine.agent.stoppingDistance-0.5f)
        {
            stateMachine.agent.isStopped = true;
        }
    }
}
