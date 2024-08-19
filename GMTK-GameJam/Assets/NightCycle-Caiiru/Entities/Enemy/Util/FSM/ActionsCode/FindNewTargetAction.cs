using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Actions/FindNewTarget")]
public class FindNewTargetAction : IAction
{
    public LayerMask targetMask;
    public GameObject[] gameObjects;
    public GameObject nearTarget;
    public override void Act(StateMachine stateMachine)
    {
        FindNewTarget(stateMachine);
    }

    public override void Enter(StateMachine stateMachine)
    {
       
    }

    private void FindNewTarget(StateMachine stateMachine)
    {
        RaycastHit[] _result = new RaycastHit[10];

        var _int = Physics.SphereCastNonAlloc(stateMachine.agent.transform.position, stateMachine.stats.visionRange, Vector3.forward, _result,
         stateMachine.stats.visionRange, targetMask, QueryTriggerInteraction.UseGlobal);
 

        if (_int > 0)
        {
            
            gameObjects = new GameObject[_int];
           nearTarget = _result[0].transform.gameObject;
           for(int i = 0; i < _int; i++){
            gameObjects[i] = _result[i].transform.gameObject; 
            if((gameObjects[i].transform.position - stateMachine.agent.transform.position).sqrMagnitude < 
                (nearTarget.transform.position - stateMachine.agent.transform.position).sqrMagnitude){
                    nearTarget = gameObjects[i];
            }

           }
           Debug.Log($"{stateMachine.transform.name} is now chasing: {nearTarget.name}");
           stateMachine.target = nearTarget.transform;
        } 
    }
}
