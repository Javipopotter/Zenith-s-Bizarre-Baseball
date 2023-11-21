using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : ConditionDad
{
    bool getDamage = false;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        getDamage = false;
        animator.GetComponent<LifesManager>().OnGetDmg.AddListener(OnGetDamage);
    }

    void OnGetDamage()
    {
        getDamage = true;
    }

    public override bool GetCondition()
    {
        return getDamage;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<LifesManager>().OnGetDmg.RemoveListener(OnGetDamage);
        getDamage = false;
    }
}
