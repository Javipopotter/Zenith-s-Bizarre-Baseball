using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu (fileName = "Shop_Item", menuName = "Shop_Item")]
public class Shop_Item_Data : UpgradeSetting
{
    public string product_name;
    public string description_key;
    [SerializeField] int amount;

    public override void Upgrade()
    {
        if(amount > 0)
        {
            amount--;
            GameManager.GM.money -= price;
            base.Upgrade();

            if(amount <= 0)
            {
                canBeSelected = false;
            }
        }
    }
}
