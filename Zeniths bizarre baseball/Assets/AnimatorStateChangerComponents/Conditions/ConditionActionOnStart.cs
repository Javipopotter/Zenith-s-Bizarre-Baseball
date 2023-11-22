using UnityEngine;

public class ConditionActionOnStart : StateMachineBehaviour
{
    [SerializeField] ConditionDad condition;
    [SerializeReference] ActionDad action;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(condition.GetCondition())
        {
            action.Action();
        }
    }
}