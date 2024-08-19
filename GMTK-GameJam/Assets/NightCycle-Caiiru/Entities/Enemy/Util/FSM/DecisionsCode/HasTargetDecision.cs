using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="FSM/Decisions/HasTargetDecision")]
public class HasTargetDecision : Decision
{
    public override bool Decide(StateMachine stateMachine)
    {
        return stateMachine.target == null ? false : true;
    }
}
