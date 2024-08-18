using System.Collections;
using System.Collections.Generic;using System.IO;
using Unity.VisualScripting;
using UnityEngine;
public class State : ScriptableObject
{ 
    public Transition[] transitions;
    public nTransition[] newTransitions;
    public virtual void OnEnter(StateMachine statemachine){ }

    public virtual void OnUpdate(StateMachine statemachine)
    {
        foreach (var _transition in newTransitions)
        {
            if (_transition.ShouldTransition(statemachine))
            {
                statemachine.SwitchState(_transition.targetState);
                break;
            }
        }
    }
    
    public virtual void OnExit(StateMachine statemachine){ }
}
[System.Serializable]
public struct nTransition
{
    public string name;
    public State targetState;
    public Condition condition;

    public bool ShouldTransition(StateMachine stateMachine)
    {
        return condition.CheckCondition(stateMachine);
    }
    
}
