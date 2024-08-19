using System.Collections;
using System.Collections.Generic; 
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Decisions/Look")]
public class LookDecision : Decision
{ 
     
    public override bool Decide(StateMachine stateMachine)
    {
        bool targetVisible = Look(stateMachine);
        return targetVisible;
    }

    private bool Look(StateMachine stateMachine)
    { 
        RaycastHit hit;
        Debug.DrawRay(stateMachine.transform.position, stateMachine.agent.transform.forward * stateMachine.stats.attackRange, Color.green);

        if (Physics.Raycast(stateMachine.agent.transform.position, stateMachine.agent.transform.forward,
                out hit, stateMachine.stats.attackRange) && hit.collider.CompareTag("Player"))
        {
            stateMachine.target = hit.transform;
            return true;
        }
        /*
        if (Physics.SphereCast(stateMachine.agent.transform.position + stateMachine.agent.transform.forward/2, stateMachine.stats.lookSphereCastRadius, stateMachine.agent.transform.forward, out hit,
                stateMachine.stats.lookSphereCastRadius) && hit.collider.CompareTag("Player"))
        {
            stateMachine.target = hit.transform;
            return true;
        }*/

        return false;
    }

     
}
