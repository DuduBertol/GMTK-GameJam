using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Actions/Attack")]
public class AttackAction : IAction
{
    public override void Act(StateMachine stateMachine)
    {
        Attack(stateMachine);
    }

    private void Attack(StateMachine stateMachine)
    {
        stateMachine.agent.isStopped = true;
        RaycastHit hit;
        Debug.DrawRay(stateMachine.transform.position, stateMachine.agent.transform.forward * stateMachine.stats.attackRange, Color.red);

        if (Physics.Raycast(stateMachine.agent.transform.position, stateMachine.agent.transform.forward,
                out hit, stateMachine.stats.attackRange) && hit.collider.CompareTag("Player"))
        {
            if (stateMachine.CheckIfCountDownElapsed(stateMachine.stats.attackRate))
            { 
                stateMachine.attackManager.Attack(stateMachine.agent.transform.forward, stateMachine.stats.attackDamage, stateMachine.stats.attackRate);
            }
        }
    }
}
