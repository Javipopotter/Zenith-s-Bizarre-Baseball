using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    string attackButton = "attack";
    string dodgeButton = "dodge";


    public static InputManager input;

    private void Awake() {
        input = this;
    }

    public bool AttackButton()
    {
        if(enabled){
            return Input.GetButtonDown(attackButton);
        }
        return false;
    }

    public bool DodgeButton()
    {
        if(enabled){
            return Input.GetButtonDown(dodgeButton);
        }
        return false;
    }
}
