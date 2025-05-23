/*using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float interactionDistance = 5f;
    [SerializeField] private KeyCode interactionKey = KeyCode.E;
    [SerializeField] private string interactionTag = "Interactable"; // <--- use tag instead of layer

    private Camera cam;
    private Outline currentOutline;
    private GameObject currentObject;
    private PuzzleInteractable currentInteractable;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        HandleRaycast();

        if (Input.GetKeyDown(interactionKey))
        {
            TryInteract();
        }
    }

    void HandleRaycast()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            GameObject hitObject = hit.collider.gameObject;

            // Only proceed if the object has the correct tag
            if (!hitObject.CompareTag(interactionTag))
            {
                ClearOutline();
                return;
            }

            Outline outline = hitObject.GetComponent<Outline>();
            PuzzleInteractable interactable = hitObject.GetComponent<PuzzleInteractable>();

            if (outline != null)
            {
                if (currentOutline != outline)
                {
                    ClearOutline();

                    currentOutline = outline;
                    currentObject = hitObject;
                    currentInteractable = interactable;

                    currentOutline.enabled = true;

                    if (currentInteractable != null)
                        currentInteractable.ShowInteractionUI();
                }
                return;
            }
        }

        ClearOutline();
    }

    void TryInteract()
    {
        if (currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }

    void ClearOutline()
    {
        if (currentOutline != null)
            currentOutline.enabled = false;

        if (currentInteractable != null)
            currentInteractable.HideInteractionUI();

        currentOutline = null;
        currentObject = null;
        currentInteractable = null;
    }
}*/
