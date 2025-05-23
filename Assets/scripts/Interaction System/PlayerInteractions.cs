using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    public float playerReach = 5f;
    MonoBehaviour currentInteractable;
    public PedestalPuzzleManager puzzleManager;
    
    private Outline currentOutline;

    void Update()
    {
        CheckInteractions();


        if (Input.GetKeyDown(KeyCode.E) && currentInteractable != null)
        {
            if (currentInteractable is Interactable i)
                i.Interact();
            else if (currentInteractable is PuzzleInteractable p)
                p.Interact();
        }
    }

    void CheckInteractions()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * playerReach, Color.red);

        if (Physics.Raycast(ray, out hit, playerReach))
        {
            if (hit.collider.CompareTag("Interactable"))
            {
                MonoBehaviour newInteractable = hit.collider.GetComponent<Interactable>() ?? 
                                             (MonoBehaviour)hit.collider.GetComponent<PuzzleInteractable>();

                if (newInteractable != null && newInteractable.enabled)
                {
                    if (currentInteractable != newInteractable)
                    {
                        ClearCurrentInteractable();
                        SetNewInteractable(newInteractable, hit.collider);
                    }
                    return;
                }
            }
        }

        ClearCurrentInteractable();
    }

    void SetNewInteractable(MonoBehaviour newInteractable, Collider collider)
{
    currentInteractable = newInteractable;

    // Fetch Outline on this collider or in children
    currentOutline = collider.GetComponent<Outline>();
    if (currentOutline == null)
        currentOutline = collider.GetComponentInChildren<Outline>(); // fallback

    if (currentOutline != null)
        currentOutline.enabled = true;

    if (newInteractable is Interactable i)
    {
        i.ShowInteractionUI();
       // if (!i.IsInteracting()) HUDController.instance.EnableInteractionPrompt();
    }
    else if (newInteractable is PuzzleInteractable p)
    {
        p.ShowInteractionUI();
       // HUDController.instance.EnableInteractionPrompt();
    }
}

    void ClearCurrentInteractable()
    {
        //HUDController.instance.DisableInteractionPrompt();

        if (currentOutline != null)
        {
            currentOutline.enabled = false;
            currentOutline = null;
        }

        if (currentInteractable != null)
        {
            if (currentInteractable is Interactable i)
                i.HideInteractionUI();
            else if (currentInteractable is PuzzleInteractable p)
                p.HideInteractionUI();
            
            currentInteractable = null;
        }
    }
}