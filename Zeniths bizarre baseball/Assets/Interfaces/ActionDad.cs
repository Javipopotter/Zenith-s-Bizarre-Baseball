using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionDad : StateMachineBehaviour
{
    [HideInInspector] public Animator an;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        an = animator;
    }

    public virtual void Action() {}
}
