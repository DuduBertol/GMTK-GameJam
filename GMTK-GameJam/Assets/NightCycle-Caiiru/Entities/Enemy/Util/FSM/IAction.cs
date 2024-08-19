using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IAction : ScriptableObject
{
    public abstract void Act(StateMachine stateMachine);
    public abstract void Enter(StateMachine stateMachine);
}
