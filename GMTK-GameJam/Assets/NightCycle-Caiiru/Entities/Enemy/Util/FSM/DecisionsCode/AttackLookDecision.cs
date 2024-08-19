using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Decisions/AttackLook")]
public class AttackLookDecision : Decision
{
    public override bool Decide(StateMachine stateMachine)
    {
        bool targetVisible = Look(stateMachine);
        return targetVisible;
    }

    private bool Look(StateMachine stateMachine)
    { 
        RaycastHit hit;
        Debug.DrawRay(stateMachine.transform.position, stateMachine.agent.transform.forward * stateMachine.stats.attackRange, Color.red);

        if (Physics.Raycast(stateMachine.agent.transform.position, stateMachine.agent.transform.forward,
                out hit, stateMachine.stats.attackRange) && hit.collider.CompareTag("Player"))
        {
            stateMachine.target = hit.transform;
            return true;
        }
       

        return false;
    }
}
