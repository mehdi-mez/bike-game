using UnityEngine;
using UnityEngine.Events;

public class PuzzleInteractable : MonoBehaviour
{
    Outline outline;
    [Header("Interaction UI")]
    public GameObject interactionImage;
    public string interactionText;

    public UnityEvent onInteraction;

    void Start()
    {
        outline = GetComponent<Outline>();
        if (outline != null)
            outline.enabled = false;

        if (interactionImage != null)
            interactionImage.SetActive(false);
    }

    public void Interact()
    {
        Debug.Log("PuzzleInteractable Interact() called on: " + gameObject.name);
        HideInteractionUI();
        onInteraction.Invoke(); // Always allow
    }

    public void ShowInteractionUI()
    {
        if (interactionImage != null)
            interactionImage.SetActive(true);


    }

    public void HideInteractionUI()
    {
        if (interactionImage != null)
            interactionImage.SetActive(false);

    }

  

    

    public bool IsInteracting()
    {
        return false; // Always interactable
    }
}
