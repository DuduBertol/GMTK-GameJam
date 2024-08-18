using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Transition")]
public class Transition : ScriptableObject
{
  public State targetState;
  public Condition condition;

  public bool ShouldTransition(StateMachine stateMachine)
  {
    return condition.CheckCondition(stateMachine);
  }
  
  
}

public abstract class Condition : ScriptableObject
{
  public abstract bool CheckCondition(StateMachine stateMachine);
}