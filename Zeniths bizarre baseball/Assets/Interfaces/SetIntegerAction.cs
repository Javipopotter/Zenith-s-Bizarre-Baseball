using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetIntegerAction : ActionDad
{
    [SerializeField] string integerName;
    [SerializeField] int value;

    public void Action(Animator an)
    {
        an.SetInteger(integerName, value);
    }
}
