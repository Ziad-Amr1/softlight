// ./Assets/ScriptableObjects/ItemData.cs

/*
Item Data
- Name
- Description
- Price
- Icon
- Max Stack 

he know itself.
*/

using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public string id;
    public string itemName;
    public string description;
    public int price;
    public Sprite icon;
    public int maxStack = 99;
    public bool isStackable => maxStack > 1;
}