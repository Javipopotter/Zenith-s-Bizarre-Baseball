using UnityEngine;

public class GetDamageCondition : ConditionDad
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        animator.GetComponent<LifesManager>().OnLifeChange.AddListener(OnGetDamage);
    }

    void OnGetDamage(float value)
    {
        animatorParameterSaver.value = true;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<LifesManager>().OnLifeChange.RemoveListener(OnGetDamage);
    }
}
