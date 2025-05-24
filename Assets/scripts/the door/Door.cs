using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Animation")]
    public Animator doorAnimator;
    public string openAnimationName = "DoorOpen"; // Must match your clip name!
    private bool hasOpened = false;

    [Header("Interaction")]
    public Interactable doorInteractable;

    void Start()
    {
        // Reset animator to idle state
        if (doorAnimator != null)
        {
            doorAnimator.Play("Idle"); // Matches your default state
        }
    }

    private void OpenDoor()
    {
        if (hasOpened || doorAnimator == null) return;

        // Play animation and lock door
        doorAnimator.Play(openAnimationName);
        hasOpened = true;

        // Optional: Disable interaction after opening
        doorInteractable.enabled = false;
        Debug.Log("Door opened!");
    }

    void OnEnable()
    {
        if (doorInteractable != null)
        {
            doorInteractable.onInteraction.AddListener(OpenDoor);
        }
    }

    void OnDisable()
    {
        if (doorInteractable != null)
        {
            doorInteractable.onInteraction.RemoveListener(OpenDoor);
        }
    }
}