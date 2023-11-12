using UnityEngine;

public class ShopItem : MonoBehaviour
{
    [SerializeField] Shop_Item_Data data;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.transform.CompareTag("Player"))
        {
            DialoguesManager.dialoguesManager.ExecuteDialogViaKey(data.description_key);
        }
        if(other.transform.CompareTag("bat"))
        {
            DialoguesManager.dialoguesManager.ExecuteDialogViaKey(data.description_key + "_Destroy");
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.transform.CompareTag("Player"))
        {
            DialoguesManager.dialoguesManager.ExecuteDialog("");
        }
    }
}
