using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/State")]
public class State : ScriptableObject
{
    public IAction[] actions;
    public Transition[] transitions;
    public void UpdateState(StateMachine stateMachine)
    {
        DoActions(stateMachine);
        CheckTransitions(stateMachine);
    }

    private void DoActions(StateMachine stateMachine)
    {
        foreach (var _action in actions)
        {
            _action.Act(stateMachine);
        }
    }

    private void CheckTransitions(StateMachine stateMachine)
    {
        for (int i = 0; i < transitions.Length; i++)
        {
            bool decisionSucceded = transitions[i].decision.Decide(stateMachine);
            if (decisionSucceded)
                stateMachine.TransitionToState(transitions[i].trueState);
            else
                stateMachine.TransitionToState(transitions[i].falseState);
            
        }
    }
}
