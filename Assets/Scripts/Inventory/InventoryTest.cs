// ./Assets/Scripts/Inventory/InventoryTest.cs
using UnityEngine;

public class InventoryTest : MonoBehaviour
{
    public InventoryController inventoryController;
    public ItemData testItem; // حط هنا الآيتم اللي عملته (زي السيف)

    void Update()
    {
        // لو ضغطت على حرف I في الكيبورد، هتضاف الآيتم في الـ Inventory
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryController.AddItem(testItem, 1);
            Debug.Log("Added 1 " + testItem.itemName);
        }
    }
}