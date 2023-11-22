using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetIntegerAction : ActionDad
{
    [SerializeField] string integerName;
    public int value;

    public void Action(Animator an)
    {
        an.SetInteger(integerName, value);
    }
}
