using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Conditions/TargetOutsideRange")]
public class TargetOutsideRangeCondition : Condition
{
    public float range;
    public float distance;
    public override bool CheckCondition(StateMachine stateMachine)
    {
        if (stateMachine == null) return false;
        
        distance = Vector3.Distance(stateMachine.transform.position, stateMachine.target.position);
         
        return distance >= range*2;
    }
}
