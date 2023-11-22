using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SumInteger : ActionDad
{
    [SerializeField] string integerName;
    [SerializeField] int value;

    public override void Action()
    {
        myTransform.GetComponent<Animator>().SetInteger(integerName, myTransform.GetComponent<Animator>().GetInteger(integerName) + value);
    }
}
