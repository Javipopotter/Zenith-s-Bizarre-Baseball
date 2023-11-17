using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] Movement movement;
    PlayerInput thisPlayerInput;

    private void Awake() {
        thisPlayerInput = GetComponent<PlayerInput>();
        movement = GetComponent<Movement>();
    }

    private void OnDisable() {
        movement.SetSpeed(Vector2.zero);
    }

    public void OnPauseActionPerformed(InputAction.CallbackContext context)
    {
        GameManager.GM.PauseGame();
    }

    public void OnMoveActionPerformed(InputAction.CallbackContext context)
    {
        movement.SetSpeed(context.ReadValue<Vector2>());
    }

    public void OnAttackActionPerformed(InputAction.CallbackContext context)
    {
        movement.Attack();
    }

    public void OnDashActionPerformed(InputAction.CallbackContext context)
    {
        movement.Dash();
    }

    public void OnPointerActionPerformed(InputAction.CallbackContext context)
    {
        if(context.control.device == InputSystem.GetDevice<Mouse>())
        {
            Vector2 mousePos = context.ReadValue<Vector2>() - (Vector2)Camera.main.WorldToScreenPoint(transform.position);
            print(mousePos);
            movement.SetPointer(mousePos);
        }
        if(context.ReadValue<Vector2>() != Vector2.zero)
        {
            movement.SetPointer(context.ReadValue<Vector2>());
        }
    }

    public void InputSetEnabled(bool value)
    {
        thisPlayerInput.enabled = value;
    }
}
