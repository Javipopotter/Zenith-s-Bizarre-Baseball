using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager input;
    [SerializeField] InputActionReference topDownMovementAction;
    [SerializeField] InputActionReference AttackAction;
    [SerializeField] InputActionReference dashAction;
    [SerializeField] InputActionReference pointerAction;
    [SerializeField] InputActionReference pauseAction;
    [SerializeField] Movement movement;

    private void Awake() {
        input = this;
        pauseAction.action.performed += OnPauseActionPerformed;
        topDownMovementAction.action.performed += OnMoveActionPerformed;
        AttackAction.action.performed += OnAttackActionPerformed;
        dashAction.action.performed += OnDashActionPerformed;
        pointerAction.action.performed += OnPointerActionPerformed;
    }

    private void OnEnable() {
        pauseAction.action.Enable();
        topDownMovementAction.action.Enable();
        AttackAction.action.Enable();
        dashAction.action.Enable();
        pointerAction.action.Enable();
    }

    private void OnDisable() {
        topDownMovementAction.action.Disable();
        AttackAction.action.Disable();
        dashAction.action.Disable();
        pointerAction.action.Disable();

        movement.SetSpeed(Vector2.zero);
    }

    private void OnDestroy() {
        pauseAction.action.performed -= OnPauseActionPerformed;
        topDownMovementAction.action.performed -= OnMoveActionPerformed;
        AttackAction.action.performed -= OnAttackActionPerformed;
        dashAction.action.performed -= OnDashActionPerformed;
        pointerAction.action.performed -= OnPointerActionPerformed;
    }

    void OnPauseActionPerformed(InputAction.CallbackContext context)
    {
        GameManager.GM.PauseGame();
    }

    void OnMoveActionPerformed(InputAction.CallbackContext context)
    {
        movement.SetSpeed(context.ReadValue<Vector2>());
    }

    void OnAttackActionPerformed(InputAction.CallbackContext context)
    {
        movement.Attack();
    }

    void OnDashActionPerformed(InputAction.CallbackContext context)
    {
        movement.Dash();
    }

    void OnPointerActionPerformed(InputAction.CallbackContext context)
    {

        if(context.ReadValue<Vector2>() != Vector2.zero)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            movement.SetPointer(context.ReadValue<Vector2>());
        }

        // if(false)
        // Cursor.visible = true;
        // Vector2 mousePosition = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
        // Vector2 direction = mousePosition - (Vector2)movement.transform.position;
        // movement.SetPointer(direction.normalized);
        // {
        // }
        // else
        // {
        // }
    }

    public void BlockActions()
    {
        AttackAction.action.Disable();
        dashAction.action.Disable();
        pointerAction.action.Disable();
    }

    public void UnblockActions()
    {
        AttackAction.action.Enable();
        dashAction.action.Enable();
        pointerAction.action.Enable();
    }
}
