using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IActionable
{
    UnityEvent OnActionPerformed { get; set; }

    public void Action() { OnActionPerformed?.Invoke(); Debug.Log("Weapon Action Executed");}
}
