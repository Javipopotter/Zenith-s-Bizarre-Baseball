using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] GameObject interactText;
    [SerializeField] GameObject shopMenu;
    [SerializeField] string welcomeKey;
    
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
    }

    void OpenShop()
    {
        shopMenu.SetActive(true);
        canInteract = false;

        DialoguesManager.dialoguesManager.pass.action.Disable();
        DialoguesManager.dialoguesManager.ExecuteDialogViaKey(welcomeKey);
    }
}
