using UnityEngine;

public class GetDamageCondition : ConditionDad
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        animator.GetComponent<LifesManager>().OnGetDmg.AddListener(OnGetDamage);
    }

    void OnGetDamage()
    {
        animatorParameterSaver.value = true;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<LifesManager>().OnGetDmg.RemoveListener(OnGetDamage);
    }
}
