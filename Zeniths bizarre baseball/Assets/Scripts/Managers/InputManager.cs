using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    Movement movement;
    AttackHandler attackHandler;
    Pointer pointer;
    PlayerInput thisPlayerInput;
    

    private void Awake() {
        thisPlayerInput = GetComponent<PlayerInput>();
        GetComponentInChildren<Pointer>();
        movement = GetComponent<Movement>();
    }

    private void Start() {
        if(GameManager.GM != null)
        {
            GameManager.GM.OnStateEnter.AddListener(OnStateEnter);
        }
    }

    private void OnDestroy() {
        if(GameManager.GM != null)
        {
            GameManager.GM.OnStateEnter.RemoveListener(OnStateEnter);
        }
    }

    void OnStateEnter(GameStates state) => InputSetEnabled(state == GameStates.playing);

    public void OnPauseActionPerformed(InputAction.CallbackContext context)
    {
        if(GameManager.GM != null) GameManager.GM.ChangeStateOfGame(GameStates.paused);
    }

    public void OnMoveActionPerformed(InputAction.CallbackContext context)
    {
        if(context.performed)
            movement.SetSpeed(context.ReadValue<Vector2>());
        else if(context.canceled)
            movement.SetSpeed(Vector2.zero);
    }

    public void OnAttackActionPerformed(InputAction.CallbackContext context)
    {
        if(context.performed) {
            attackHandler.UseWeapon();
        }
    }

    public void OnDashActionPerformed(InputAction.CallbackContext context)
    {
        if(context.performed) movement.Dash();
    }

    public void OnPointerActionPerformed(InputAction.CallbackContext context)
    {
        if(context.control.device == InputSystem.GetDevice<Mouse>())
        {
            Vector2 mousePos = context.ReadValue<Vector2>() - (Vector2)Camera.main.WorldToScreenPoint(transform.position);
            pointer.SetPointer(mousePos);
        }
        else if(context.ReadValue<Vector2>() != Vector2.zero)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            pointer.SetPointer(context.ReadValue<Vector2>());
        }
    }

    void InputSetEnabled(bool value)
    {
        thisPlayerInput.enabled = value;
    }
}
