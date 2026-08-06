// ./Assets/Scripts/UI/InventorySlotUI.cs

/*
Inventory Slot UI
- Selection
- Highlight
- Hover

he don't know Image(Icon) or TMP_Text(count).
*/

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text countText;
    [SerializeField] private Image selection;

    private void Awake()
    {
        ClearSlot();
    }

    public int SlotIndex { get; private set; }
    // تهيئة الخانة برقمها والكنترولر
    public void Initialize(int index)
    {
        SlotIndex = index;
    }

    /// <summary>
    /// يعرض عنصر داخل الخانة.
    /// </summary>
    public void SetSlot(Sprite itemIcon, int amount)
    {
        if (itemIcon == null)
        {
            ClearSlot();
            return;
        }

        icon.sprite = itemIcon;
        icon.enabled = true;

        if (amount > 1)
        {
            countText.text = amount.ToString();
            countText.enabled = true;
        }
        else
        {
            countText.text = "";
            countText.enabled = false;
        }
    }

    /// <summary>
    /// يجعل الخانة فارغة.
    /// </summary>
    public void ClearSlot()
    {
        icon.sprite = null;
        icon.enabled = false;

        countText.text = "";
        countText.enabled = false;

        if (selection != null)
            selection.enabled = false;
    }

    /// <summary>
    /// إظهار أو إخفاء تحديد الخانة.
    /// </summary>
    public void SetSelected(bool value)
    {
        if (selection != null)
            selection.enabled = value;
    }
}