using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    public float playerReach = 5f;
    MonoBehaviour currentInteractable;
    public PedestalPuzzleManager puzzleManager;

    private Outline currentOutline;

    void Update()
    {
        CheckInteractions(); // This is what was missing

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentInteractable != null)
            {
                if (currentInteractable is Interactable i)
                    i.Interact();
                else if (currentInteractable is PuzzleInteractable p)
                    p.Interact();
            }
            else
            {
                // Try mounting a bike
                Ray ray = new Ray(transform.position, transform.forward);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, playerReach))
                {
                    var bike = hit.collider.GetComponent<BikeMountManager>();
                    if (bike != null && !bike.IsMounted())
                    {
                        bike.MountBike();
                    }
                }
            }
        }
    }

    void CheckInteractions()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * playerReach, Color.red);

        RaycastHit hit;
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

        currentOutline = collider.GetComponent<Outline>();
        if (currentOutline == null)
            currentOutline = collider.GetComponentInChildren<Outline>();

        if (currentOutline != null)
            currentOutline.enabled = true;

        if (newInteractable is Interactable i)
        {
            i.ShowInteractionUI();
        }
        else if (newInteractable is PuzzleInteractable p)
        {
            p.ShowInteractionUI();
        }
    }

    void ClearCurrentInteractable()
    {
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
