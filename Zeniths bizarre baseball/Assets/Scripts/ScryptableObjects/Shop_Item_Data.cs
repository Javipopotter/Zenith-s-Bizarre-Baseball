using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu (fileName = "Shop_Item", menuName = "Shop_Item")]
public class Shop_Item_Data : UpgradeSetting
{
    public string product_name;
    public string description_key;

    public override void Upgrade()
    {

        GameManager.GM.money -= price;
        base.Upgrade();
    }
}
