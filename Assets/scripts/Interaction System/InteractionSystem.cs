using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float interactionDistance = 5f;
    [SerializeField] private KeyCode interactionKey = KeyCode.E;
    [SerializeField] private LayerMask interactionLayer;

    private Camera cam;
    private Outline currentOutline;
    private GameObject currentObject;

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

        Debug.Log("Interaction system is running");

    }

    void HandleRaycast()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance, interactionLayer))
        {
            GameObject hitObject = hit.collider.gameObject;
            Outline outline = hitObject.GetComponent<Outline>();

            if (outline != null)
            {
                if (currentOutline != outline)
                {
                    ClearOutline();
                    currentOutline = outline;
                    currentObject = hitObject;
                    currentOutline.enabled = true;
                }
                return;
            }
        }
        if (Physics.Raycast(ray, out hit, interactionDistance, interactionLayer))
        {
            Debug.Log("Ray hit: " + hit.collider.name);
        }
        else
        {
            Debug.Log("Ray hit nothing");
        }


        ClearOutline();
    }

    void TryInteract()
    {
        if (currentObject != null)
        {
            IInteractable interactable = currentObject.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }

    void ClearOutline()
    {
        if (currentOutline != null)
        {
            currentOutline.enabled = false;
            currentOutline = null;
            currentObject = null;
        }
    }
}
