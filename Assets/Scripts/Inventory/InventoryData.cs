// ./Assets/Scripts/Inventory/InventoryData.cs
using System.Collections.Generic;
using UnityEngine;

public class InventoryData
{
    private InventoryItem[] items; // Array بتاعة الـ Slots

    public int SlotCount => items.Length;

    public InventoryData(int slotCount)
    {
        items = new InventoryItem[slotCount];
    }

    // الحصول على آيتم في خانة معينة
    public InventoryItem GetItem(int index)
    {
        if (index < 0 || index >= items.Length) return null;
        return items[index];
    }

    // إضافة آيتم جديد (يرجع الباقي لو الـ Inventory ممتلئ)
    public int AddItem(ItemData itemToAdd, int amount = 1)
    {
        // 1. دور على Stack موجود عشان تزود فيه
        if (itemToAdd.isStackable)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] != null && items[i].data == itemToAdd && items[i].stackCount < itemToAdd.maxStack)
                {
                    int spaceLeft = itemToAdd.maxStack - items[i].stackCount;
                    int toAdd = Mathf.Min(amount, spaceLeft);

                    items[i].AddToStack(toAdd);
                    amount -= toAdd;

                    if (amount <= 0) return 0; // كل الكمية اتضافت
                }
            }
        }

        // 2. دور على خانة فاضية عشان تحط الباقي
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null || items[i].IsEmpty)
            {
                int toAdd = Mathf.Min(amount, itemToAdd.maxStack);
                items[i] = new InventoryItem(itemToAdd, toAdd);
                amount -= toAdd;

                if (amount <= 0) return 0;
            }
        }

        // لو رجع رقم أكبر من صفر، يبقى الـ Inventory ممتلين ومقدرش ياخد كل الكمية
        return amount;
    }

    // مسح آيتم من خانة
    public void RemoveItem(int index, int amount = 1)
    {
        if (items[index] == null) return;

        items[index].stackCount -= amount;
        if (items[index].stackCount <= 0)
        {
            items[index] = null; // الخانة بقت فاضية
        }
    }

    // ترتيب الآيتمات (Swap) بين خانتين
    public void SwapItems(int index1, int index2)
    {
        if (index1 == index2) return;

        InventoryItem temp = items[index1];
        items[index1] = items[index2];
        items[index2] = temp;
    }

    public List<InventorySlotSaveData> ExportSaveData()
    {
        var saveData = new List<InventorySlotSaveData>();
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != null && !items[i].IsEmpty)
            {
                saveData.Add(new InventorySlotSaveData
                {
                    itemId = items[i].data.id,
                    amount = items[i].stackCount,
                    slotIndex = i,
                });
            }
        }
        return saveData;
    }

    private void SetItem(int slotIndex, ItemData item, int amount)
    {
        items[slotIndex] = new InventoryItem(item, amount);
    }

    public void ImportSaveData(
    List<InventorySlotSaveData> saveData,
    ItemDatabase itemDatabase)
    {
        // امسح الـ Inventory الحالية أولًا
        items = new InventoryItem[items.Length];

        foreach (InventorySlotSaveData data in saveData)
        {
            ItemData item = itemDatabase.FindItemById(data.itemId);

            if (item == null)
            {
                Debug.LogWarning($"Item '{data.itemId}' was not found.");
                continue;
            }

            if (data.slotIndex < 0 || data.slotIndex >= items.Length)
            {
                Debug.LogWarning($"Invalid slot index: {data.slotIndex}");
                continue;
            }

            SetItem(data.slotIndex, item, data.amount);

            Debug.Log($"Slot {data.slotIndex}: {GetItem(data.slotIndex)?.data?.itemName} x{GetItem(data.slotIndex)?.stackCount}");
        }
    }
}