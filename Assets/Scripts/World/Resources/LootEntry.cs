// ./Assets/Scripts/World/Resources/LootEntry.cs
using UnityEngine;

[System.Serializable]
public class LootEntry
{
    public ItemData item;
    public int minAmount = 1;
    public int maxAmount = 1;
    [Range(0, 100)]
    public float chance = 100f;
}