using UnityEngine;
using TMPro;

public class SetText : MonoBehaviour
{
    [SerializeField] UpgradeSetting upgradeSetting;
    public Shop_Item_Data shopItemData;

    private void OnEnable() {
        if(upgradeSetting != null)
        {
            GetComponentInChildren<TextMeshProUGUI>().text = upgradeSetting.description;
        }
        else if(shopItemData != null)
        {
            GetComponentInChildren<TextMeshProUGUI>().text = shopItemData.description;
        }
    }
    
}
