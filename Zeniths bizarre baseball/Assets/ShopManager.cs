using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] Transform ItemsContainer;
    [SerializeField] GameObject interactText;
    [SerializeField] GameObject ShopMenu;
    [SerializeField] string welcomeKey;
    bool canInteract = false;

    private void OnEnable() {
        for(int i = 0; i < ItemsContainer.childCount; i++)
        {
            Transform child = ItemsContainer.GetChild(i);
            if(child.GetComponent<SetText>().shopItemData.price > GameManager.GM.money)
            {
                child.GetComponent<Button>().interactable = false;
            }
            else
            {
                child.GetComponent<Button>().interactable = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.transform.CompareTag("Player"))
        {
            canInteract = true;
            InputManager.input.enabled = false;
        }
    }

    private void Update() {
        if(canInteract)
        {
            interactText.SetActive(true);
            if(Input.GetKeyDown(KeyCode.Space))
            {
                ShopMenu.SetActive(true);
                DialoguesManager.dialoguesManager.ExecuteDialogViaKey(welcomeKey);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.transform.CompareTag("Player"))
        {
            canInteract = false;
            interactText.SetActive(false);
            CloseShop();
        }
    }

    public void CloseShop()
    {
        InputManager.input.enabled = true;
        ShopMenu.SetActive(false);
        DialoguesManager.dialoguesManager.ExecuteDialog("");
    }
}
