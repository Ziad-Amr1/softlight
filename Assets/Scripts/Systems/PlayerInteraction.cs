using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    // =========================
    // References
    // =========================

    private PlayerControls input;
    private PlayerMovement movement;

    [SerializeField]
    private GameObject interactUI;

    // =========================
    // State
    // =========================

    private IInteractable currentTarget;
    private List<IInteractable> interactablesInRange = new();

    // =========================
    // Unity Lifecycle
    // =========================

    private void Awake()
    {
        // Create Input
        input = new PlayerControls();
        // Get PlayerMovement
        movement = GetComponent<PlayerMovement>();
        // Hide UI
        interactUI.SetActive(false);
    }

    private void OnEnable()
    {
        // Enable Input
        input.Gameplay.Enable();
    }

    private void OnDisable()
    {
        // Disable Input
        input.Gameplay.Disable();
    }

    private void Update()
    {
        // Read Interact Input
        bool interactTriggered = input.Gameplay.Interact.triggered;

        // If there is a current target
        if (currentTarget != null && interactTriggered)
        {
            // Interact
            currentTarget.Interact();
        }
    }

    // =========================
    // Trigger Detection
    // =========================

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Get IInteractable
        IInteractable interactable = other.GetComponent<IInteractable>();

        // Add to interactablesInRange

        // ChooseBestTarget()

        // UpdateInteractionUI()


        if (interactable != null)
        {
            interactablesInRange.Add(interactable);
            ChooseBestTarget();
            UpdateInteractionUI();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Get IInteractable
        IInteractable interactable = other.GetComponent<IInteractable>();

        // Remove from interactablesInRange
        if (interactable != null)
        {
            interactablesInRange.Remove(interactable);
        }
        // ChooseBestTarget()
        ChooseBestTarget();
        // UpdateInteractionUI()
        UpdateInteractionUI();
    }

    // =========================
    // Internal Logic
    // =========================

    private void ChooseBestTarget()
    {
        // لا يوجد أي عنصر داخل الرينج
        if (interactablesInRange.Count == 0)
        {
            currentTarget = null;
            return;
        }

        IInteractable bestTarget = null;
        float bestDistance = float.MaxValue;

        foreach (IInteractable interactable in interactablesInRange)
        {
            float distance = Vector2.Distance(
                transform.position,
                interactable.Position
            );

            if (distance < bestDistance)
            {
                bestDistance = distance;
                bestTarget = interactable;
            }
        }

        currentTarget = bestTarget;
    }

    private void UpdateInteractionUI()
    {
        if (currentTarget == null)
        {
            interactUI.SetActive(false);
            return;
        }

        interactUI.SetActive(true);

        Vector3 screenPosition =
            Camera.main.WorldToScreenPoint(
                currentTarget.Position + Vector2.up * 1.2f
            );

        interactUI.transform.position = screenPosition;
    }
}