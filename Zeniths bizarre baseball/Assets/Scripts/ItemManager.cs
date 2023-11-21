using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class ItemManager : MonoBehaviour
{
    [SerializeField] UpgradeSetting upgradeSetting;
    [SerializeField] Image icon;
    int amount = 1;

    private void OnEnable() {
        amount = 1;
        upgradeSetting.OnUpgrade.AddListener(OnUpgrade);
        if(upgradeSetting != null)
        {
            string description = upgradeSetting.description;
            icon.sprite = upgradeSetting.icon;

            if(upgradeSetting.price > 0)
            {
                description += "\n" + upgradeSetting.price + " monedas";
            }

            GetComponentInChildren<TextMeshProUGUI>().text = description;
        }
    }

    private void OnDisable() {

        GetComponentInChildren<TextMeshProUGUI>().text = "";
        upgradeSetting.OnUpgrade.RemoveListener(OnUpgrade);
    }

    void OnUpgrade()
    {
        amount--;
        if(amount <= 0){
            // GetComponent<Button>().animator.SetTrigger("Disabled");
            GetComponent<Button>().onClick.RemoveAllListeners();
        }
    }
}
