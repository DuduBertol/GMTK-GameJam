using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="FSM/Actions/BackHomeAction")]
public class BackHomeAction : IAction
{
    public override void Act(StateMachine stateMachine)
    { 
        stateMachine.agent.isStopped = false;
        stateMachine.agent.destination = stateMachine.initialPosition;
    }

    public override void Enter(StateMachine stateMachine)
    { 
    }

     
}
