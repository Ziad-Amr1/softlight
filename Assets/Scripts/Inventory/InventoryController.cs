// ./Assets/Scripts/Inventory/InventoryController.cs

/*
Inventory Controller
- Add Item
- Remove Item
- Move Item
- Swap
- Inventory Data

Don't know Image or TMP_Text.
*/
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InventoryUI inventoryUI;

    [Header("Settings")]
    [SerializeField] private int slotCount = 40;

    // الـ Model (الداتا الحقيقية)
    public InventoryData Data { get; private set; }

    private void Start()
    {
        // 1. بناء الداتا
        Data = new InventoryData(slotCount);

        // 2. بناء الـ UI
        inventoryUI.CreateSlots(slotCount);

        // 3. عمل Refresh عشان يطابق الداتا (في البداية هتكون فاضية)
        RefreshUI();
    }

    // إضافة آيتم (للاستخدام من السكريبتات التانية زي PlayerInteraction)
    public void AddItem(ItemData item, int amount = 1)
    {
        int remaining = Data.AddItem(item, amount);
        if (remaining > 0)
        {
            Debug.Log($"Inventory Full! Couldn't add {remaining} {item.itemName}s.");
        }
        RefreshUI();
    }

    // مسح آيتم
    public void RemoveItem(int slotIndex, int amount = 1)
    {
        Data.RemoveItem(slotIndex, amount);
        RefreshUI();
    }

    // تحديث الشاشة بناءً على الداتا
    public void RefreshUI()
    {
        Debug.Log("=== RefreshUI ===");

        for (int i = 0; i < Data.SlotCount; i++)
        {
            InventoryItem item = Data.GetItem(i);
            InventorySlotUI slotUI = inventoryUI.GetSlot(i);

            if (slotUI == null)
            {
                Debug.LogError($"Slot {i} is NULL!");
                continue;
            }

            if (item != null)
            {
                Debug.Log($"Refreshing Slot {i}: {item.data.itemName} x{item.stackCount}");
            }

            if (item != null && !item.IsEmpty)
            {
                slotUI.SetSlot(item.data.icon, item.stackCount);
            }
            else
            {
                slotUI.ClearSlot();
            }
        }
    }

    // ترتيب الآيتمات (للاستخدام من الـ Drag Handler)
    public void SwapItems(int sourceIndex, int targetIndex)
    {
        Data.SwapItems(sourceIndex, targetIndex);
        RefreshUI();
    }
}