using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "FSM/Conditions/TargetInRange")]
public class TargetInRangeCondition : Condition
{
    public float range;
    public float distance;
    public override bool CheckCondition(StateMachine stateMachine)
    {
        if (stateMachine == null) return false;
        Debug.DrawRay(stateMachine.GetAgent().transform.position, stateMachine.GetAgent().transform.forward * range, Color.cyan);
         
        distance = Vector3.Distance(stateMachine.transform.position, stateMachine.target.position);
         
        return distance <= range*2;
    }
}
