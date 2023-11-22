using UnityEngine;

public class ConditionActionOnUpdate : StateMachineBehaviour
{
    [SerializeField] ConditionDad[] condition;
    [SerializeReference] ActionDad[] action;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        for(int i = 0; i < condition.Length; i++)
        {
            condition[i].an = animator;
            action[i].an = animator;
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        for(int i = 0; i < condition.Length; i++)
        {
            if(condition[i].GetCondition())
            {
                action[i].Action();
            }
        }
    }
}

