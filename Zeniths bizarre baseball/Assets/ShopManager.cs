using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] Transform ItemsContainer;
    [SerializeField] GameObject interactText;
    [SerializeField] GameObject ShopMenu;

    private void OnEnable() {
        for(int i = 0; i < ItemsContainer.childCount; i++)
        {
            if(ItemsContainer.GetChild(i).GetComponent<SetText>().shopItemData.price > GameManager.GM.money)
            {
                ItemsContainer.GetComponent<Button>().interactable = false;
            }
            else
            {
                ItemsContainer.GetComponent<Button>().interactable = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.transform.CompareTag("Player"))
        {
            interactText.SetActive(true);
            if(Input.GetButtonDown("dodge"))
            {
                ShopMenu.SetActive(true);
                DialoguesManager.dialoguesManager.ExecuteDialogViaKey("ShopWelcome_Rain");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.transform.CompareTag("Player"))
        interactText.SetActive(false);
        {
            CloseShop();
        }
    }

    public void CloseShop()
    {
        ShopMenu.SetActive(false);
        DialoguesManager.dialoguesManager.ExecuteDialog("");
    }
}
