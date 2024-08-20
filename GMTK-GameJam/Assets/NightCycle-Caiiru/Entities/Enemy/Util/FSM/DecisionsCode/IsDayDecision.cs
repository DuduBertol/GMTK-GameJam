using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="FSM/Decisions/IsDayDecision")]
public class IsDayDecision : Decision
{
    public override bool Decide(StateMachine stateMachine)
    {
        return GameController.Instance.GetIsDay();


    }

    
}
