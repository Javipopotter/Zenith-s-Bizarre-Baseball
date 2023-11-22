using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionDad : StateMachineBehaviour
{
    [HideInInspector] public Transform myTransform;
    [HideInInspector] public Animator an;

    public virtual void Action() {}
}
