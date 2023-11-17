using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] Transform ItemsContainer;
    [SerializeField] GameObject interactText;
    [SerializeField] GameObject shopMenu;
    [SerializeField] string welcomeKey;
    [SerializeField] InputActionReference interactAction;
    
    bool _canInteract;
    bool canInteract
    {
        get
        {
            return _canInteract;
        }
        set
        {
            _canInteract = value;
            if(value)
            {
                interactAction.action.Enable();
                interactText.SetActive(true);
            }
            else
            {
                interactAction.action.Disable();
                interactText.SetActive(false);
            }
        }
    }

    private void OnDestroy() {
        interactAction.action.performed -= OnInteractActionPerformed;
    }

    private void OnEnable() {
        interactAction.action.performed += OnInteractActionPerformed;
    }

    private void OnDisable() {
        interactAction.action.performed -= OnInteractActionPerformed;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.transform.CompareTag("Player"))
        {
            canInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.transform.CompareTag("Player"))
        {
            CloseShop();
            canInteract = false;
        }
    }

    public void CloseShop()
    {
        shopMenu.SetActive(false);
        canInteract = true;

        DialoguesManager.dialoguesManager.pass.action.Enable();
        DialoguesManager.dialoguesManager.ExecuteDialog("");
    }

    void OpenShop()
    {
        shopMenu.SetActive(true);
        canInteract = false;
        print("interacted");
        DialoguesManager.dialoguesManager.pass.action.Disable();
        DialoguesManager.dialoguesManager.ExecuteDialogViaKey(welcomeKey);
    }
    
    void OnInteractActionPerformed(InputAction.CallbackContext context)
    {
        OpenShop();
    }
}
