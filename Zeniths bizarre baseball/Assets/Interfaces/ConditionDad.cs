using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionDad : StateMachineBehaviour
{
    public virtual bool GetCondition()
    {
        return false;
    }
}
