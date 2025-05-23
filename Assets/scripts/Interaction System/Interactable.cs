using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI; // Added for Image component
using UnityEngine.AI;
using System.Collections;

public class Interactable : MonoBehaviour
{
    // Visual Feedback
    Outline outline;
    [Header("Interaction UI")]
    public GameObject interactionImage; // Assign your "Press E" image GameObject
    public string interactionText; // Kept for event systems if needed

    // Events
    public UnityEvent onInteraction;
    private bool isInteracting = false;

    void Start()
    {
        outline = GetComponent<Outline>();
        if (outline != null)
            outline.enabled = false;

        // Initialize interaction image
        if (interactionImage != null)
            interactionImage.SetActive(false);
    }

    public void Interact()
    {
        Debug.Log("Interact() called on: " + gameObject.name);
        if (!isInteracting)
        {
            isInteracting = true;
            HideInteractionUI();
            onInteraction.Invoke();
            Debug.Log("Event invoked: " + onInteraction.GetPersistentEventCount() + " listeners");
        }
    }

    // New UI control methods
    public void ShowInteractionUI()
    {
        if (interactionImage != null)
            interactionImage.SetActive(true);

        EnableOutline();
    }

    public void HideInteractionUI()
    {
        if (interactionImage != null)
            interactionImage.SetActive(false);

        DisableOutline();
    }

    // Existing outline methods
    public void DisableOutline()
    {
        if (outline != null)
            outline.enabled = false;
    }

    public void EnableOutline()
    {
        if (outline != null)
            outline.enabled = true;
    }

    public bool IsInteracting()
    {
        return isInteracting;
    }

    public void ResetInteraction()
    {
        isInteracting = false;
    }
}