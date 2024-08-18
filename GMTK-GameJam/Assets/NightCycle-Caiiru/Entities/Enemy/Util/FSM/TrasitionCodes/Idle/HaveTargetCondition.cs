using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Conditions/HaveTarget")]
public class HaveTargetCondition : Condition
{
    
    public override bool CheckCondition(StateMachine stateMachine)
    {
        if (stateMachine.target != null)
        {
            return true;
        }

        return false;
    }
}
