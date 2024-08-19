using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="FSM/Decisions/ActiveState")]
public class ActiveStateDecision : Decision
{
    public override bool Decide(StateMachine stateMachine)
    {
        bool chaseTargetIsActive = stateMachine.target.gameObject.activeSelf;
        return chaseTargetIsActive;
    }
}
