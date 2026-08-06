// ./Assets/Scripts/Inventory/InventoryItem.cs
/*
Inventory Item
- Icon Image
- Count Text
- ItemData
- Stack Count

he know itself.
*/


// ده Instance من الآيتم اللي موجود في الـ Inventory
public class InventoryItem
{
    public ItemData data; // معلومات الآيتم الأساسية
    public int stackCount; // العدد الحالي

    // Constructor
    public InventoryItem(ItemData itemData, int amount = 1)
    {
        data = itemData;
        stackCount = amount;
    }

    // هل الخانة فاضية؟
    public bool IsEmpty => data == null;

    // إضافة عدد للـ Stack
    public void AddToStack(int amount)
    {
        stackCount += amount;
        if (stackCount > data.maxStack) stackCount = data.maxStack;
    }
}