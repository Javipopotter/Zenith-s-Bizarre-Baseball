using UnityEngine;

[CreateAssetMenu (fileName = "Shop_Item", menuName = "Shop_Item")]
public class Shop_Item_Data : ScriptableObject
{
    public string product_name;
    public string description_key;
    public int price;
}
