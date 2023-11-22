using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventAction : ActionDad
{
    [SerializeField] string MethodName;
    public override void Action()
    {
        myTransform.GetComponent<Animator>().GetType().GetMethod(MethodName).Invoke(myTransform.GetComponent<Animator>(), null);
    }
}
