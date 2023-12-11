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
        GetComponent<Button>().onClick.AddListener(OnUpgrade);
        amount = 1;
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
        GetComponent<Button>().onClick.RemoveListener(OnUpgrade);
        GetComponentInChildren<TextMeshProUGUI>().text = "";
    }

    void OnUpgrade()
    {
        upgradeSetting.Upgrade();
        amount--;
        if(amount <= 0){
            GetComponent<Button>().onClick.RemoveAllListeners();
        }
    }
}
