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

    public override void Enter(StateMachine stateMachine)
    {
        // Setar o destination apenas uma vez pois são alvos parados. 
        
    }

    private void Chase(StateMachine stateMachine)
    {
        
        if (stateMachine.target == null) return;
        if (!stateMachine.CheckIfCountDownElapsed(stateMachine.stats.movementRate))
            return;
        var _offset = Random.Range(-2, 2);
        stateMachine.agent.destination = new Vector3(stateMachine.target.transform.position.x, stateMachine.agent.transform.position.y,stateMachine.target.transform.position.z); 
        stateMachine.agent.isStopped = false;
        if (stateMachine.agent.remainingDistance < stateMachine.agent.stoppingDistance-0.5f)
        {
            stateMachine.agent.isStopped = true;
        }
    }
}
