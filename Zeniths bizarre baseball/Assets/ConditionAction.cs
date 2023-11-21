using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;


public class ConditionAction : StateMachineBehaviour
{
    [SerializeField] ConditionDad condition;
    [SerializeReference] ActionDad action;

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(condition.GetCondition())
        {
            action.Action();
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}
}

