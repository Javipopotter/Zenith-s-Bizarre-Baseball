using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SetText : MonoBehaviour
{
    [SerializeField] UpgradeSetting upgradeSetting;
    [SerializeField] Image icon;
    public Shop_Item_Data shopItemData;

    private void OnEnable() {
        if(upgradeSetting != null)
        {
            GetComponentInChildren<TextMeshProUGUI>().text = upgradeSetting.description;
            icon.sprite = upgradeSetting.icon;
        }
        else if(shopItemData != null)
        {
            GetComponentInChildren<TextMeshProUGUI>().text = shopItemData.description + "\n" + shopItemData.price + " monedas";
            icon.sprite = upgradeSetting.icon;
        }
    }
}
