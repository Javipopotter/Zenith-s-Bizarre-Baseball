using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCondition : ConditionDad
{
    [SerializeField] float timer = 5;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        timer = 5;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer -= Time.deltaTime;
    }

    public override bool GetCondition()
    {
        return timer <= 0;
    }
}
// teamorompebrazos