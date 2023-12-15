using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    Animator an;

    [SerializeField] string ROTATION_NAME = "rotation";
    [SerializeField] string ACTION_NAME = "action";

    private void Awake() {
        an = GetComponent<Animator>();
    }

    private void Start() {
        GetComponentInChildren<Pointer>().onRotChange.AddListener(RotUpdater);
        GetComponentInChildren<AttackHandler>().OnUseWeapon.AddListener(ActionUpdater);
    }

    void RotUpdater(float value)
    {
        an.SetFloat(ROTATION_NAME, value);
    }

    void ActionUpdater()
    {
        an.SetTrigger(ACTION_NAME);
    }
}
