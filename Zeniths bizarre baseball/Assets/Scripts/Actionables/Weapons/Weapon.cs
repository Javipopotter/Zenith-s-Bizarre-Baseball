using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Weapon : MonoBehaviour, IActionable
{
    public UnityEvent OnActionPerformed {get; set;}
    public float damage = 1;
    public float knockBack = 1;
    public float baseCoolDown = 0.3f;

    private void Awake() {
        OnActionPerformed = new UnityEvent();
    }

    public virtual void Action() {}
}
