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
                out hit, stateMachine.stats.attackRange) && hit.collider.CompareTag("Defense"))
        { 
            stateMachine.target = hit.transform;
            return true;
        } 

        return false;
    }

     
}
