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
    [SerializeField] GameObject firstSelected;
    
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
                interactText.SetActive(true);
            }
            else
            {
                interactText.SetActive(false);
            }
        }
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
        GameManager.GM.SetAllPLayerInputs(true);
    }

    void OpenShop()
    {
        shopMenu.SetActive(true);
        print(firstSelected.name);
        canInteract = false;

        DialoguesManager.dialoguesManager.pass.action.Disable();
        DialoguesManager.dialoguesManager.ExecuteDialogViaKey(welcomeKey);
    }
}
