using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTriggerAction : ActionDad
{
    [SerializeField] string triggerName;

    public override void Action()
    {
        an.SetTrigger(triggerName);
    }
}
