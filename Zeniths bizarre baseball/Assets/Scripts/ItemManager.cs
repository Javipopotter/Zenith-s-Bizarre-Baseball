using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class ItemManager : MonoBehaviour
{
    [SerializeField] UpgradeSetting upgradeSetting;
    [SerializeField] Image icon;

    private void OnEnable() {
        upgradeSetting.OnUpgrade.AddListener(OnUpgrade);
        if(upgradeSetting != null)
        {
            GetComponentInChildren<TextMeshProUGUI>().text = upgradeSetting.description;
            icon.sprite = upgradeSetting.icon;

            if(upgradeSetting.price > 0)
            {
                upgradeSetting.description += "\n" + upgradeSetting.price + " monedas";
            }
        }
    }

    void OnUpgrade()
    {
        if(upgradeSetting.canBeSelected == false)
        {
            GetComponent<Button>().animator.SetTrigger("Disabled");
        }
    }
}
