// ./Assets/Scripts/UI/itemDragHandler.cs

/*
Item Drag Handler

Responsibilities:
- Handle Begin Drag
- Handle Drag
- Handle End Drag
- Notify InventoryController when an item is dropped on another slot

It does not modify inventory data directly.
*/
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform originalParent;
    private CanvasGroup canvasGroup;
    private InventorySlotUI mySlotUI;

    private InventoryController controller;

    private Image iconImage;

    void Awake()
    {
        iconImage = GetComponent<Image>();

        if (iconImage == null)
        {
            Debug.LogError($"Image is missing on {gameObject.name}");
            enabled = false;
            return;
        }
        controller = FindFirstObjectByType<InventoryController>();
        canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup == null)
        {
            Debug.LogError($"CanvasGroup is missing on {gameObject.name}");
            enabled = false;
            return;
        }
        // بنجيب الـ InventorySlotUI اللي هو الأب المباشر للأيقون
        mySlotUI = GetComponentInParent<InventorySlotUI>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // لو الخانة فاضية، منسحبش حاجة
        if (mySlotUI == null || iconImage == null || iconImage.sprite == null) return;

        originalParent = transform.parent;
        transform.SetParent(transform.root); // نطلعه فوق كل حاجة عشان يبانش وهو بيتم سحبه
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position; // يتبع الماوس
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;

        // نرجعه مكانه الأصلي بصرياً الأول
        transform.SetParent(originalParent);
        GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        // دور على الخانة اللي سيبت الماوس فيها (Drop Target)
        if (eventData.pointerEnter != null)
        {
            InventorySlotUI targetSlot = eventData.pointerEnter.GetComponentInParent<InventorySlotUI>();

            // لو لقينا خانة تانية مختلفة عن الأولى، قول للـ Controller يعمل Swap
            if (targetSlot == null)
                return;

            if (targetSlot == mySlotUI)
                return;

            if (controller == null)
                return;

            controller.SwapItems(mySlotUI.SlotIndex, targetSlot.SlotIndex);
        }
    }
}