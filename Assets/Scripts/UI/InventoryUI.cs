// ./Assets/Scripts/UI/InventoryUI.cs

/*
Inventory UI
- Create Slots
- Update Screen/UI
- Refresh()

he talk to the InventoryController
*/
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [Header("Slot Settings")]
    [SerializeField] private InventorySlotUI slotPrefab;
    [SerializeField] private Transform gridParent;

    private readonly List<InventorySlotUI> slots = new();

    // الـ Controller هو اللي بينادي عليك دلوقتي
    // محتاجين نضيف Reference للـ Controller عشان نباصيه للـ Slots
    public void CreateSlots(int count)
    {
        foreach (Transform child in gridParent)
        {
            Destroy(child.gameObject);
        }
        slots.Clear();

        for (int i = 0; i < count; i++)
        {
            InventorySlotUI slot = Instantiate(slotPrefab, gridParent);
            slot.name = $"Slot ({i})";
            
            // الجديد: باصي الـ Controller والـ Index لكل Slot
            slot.Initialize(i); 

            slots.Add(slot);
        }
    }

    public InventorySlotUI GetSlot(int index)
    {
        if (index < 0 || index >= slots.Count) return null;
        return slots[index];
    }
}