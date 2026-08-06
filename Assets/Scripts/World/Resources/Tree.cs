// ./Assets/Scripts/World/Resources/Tree.cs
using UnityEngine;

public class Tree : MonoBehaviour, IInteractable
{
    [SerializeField]
    private TreeData data;
    public Vector2 Position => transform.position;

    private InventoryController inventory;

    private void Awake()
    {
        inventory = FindFirstObjectByType<InventoryController>();
    }

    public void Interact()
    {
        // هنا ضع الكود الذي يحدث عند التفاعل مع الشجرة
        foreach (var loot in data.lootTable)
        {
            float chanceRoll = Random.Range(0f, 100f);
            if (chanceRoll > loot.chance)
            {
                continue;
            }

            int amount = Random.Range(loot.minAmount, loot.maxAmount + 1);
            inventory.AddItem(loot.item, amount);
            Debug.Log($"Rolled {chanceRoll} and got a {loot.item.itemName}\n with {amount} amount");

        }

        Debug.Log("Interacted with the tree!");
    }
}