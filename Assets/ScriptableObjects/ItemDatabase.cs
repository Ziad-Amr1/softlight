// ./Assets/ScriptableObjects/ItemDatabase.cs
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Data/Item Database")]
public class ItemDatabase : ScriptableObject
{
    public List<ItemData> items = new();

    public ItemData FindItemById(string id)
{
    foreach (ItemData item in items)
    {
        if (item.id == id)
        {
            return item;
        }
    }

    return null;
}
}