using UnityEngine;

public class TriggerInteraction : MonoBehaviour, IInteractable
{
    public GameObject objectToDeactivate;
    public GameObject objectToActivate;
    public MonoBehaviour scriptToActivate;

    public float delayBeforeDeactivate = 0.1f;

    public void Interact()
    {
        Debug.Log("Triggered interaction!");

        // First activate new stuff
        if (objectToActivate != null)
            objectToActivate.SetActive(true);

        if (scriptToActivate != null)
            scriptToActivate.enabled = true;

        // Delay deactivation so this script finishes executing
        if (objectToDeactivate != null)
            StartCoroutine(DelayedDeactivate());

        Outline outline = GetComponent<Outline>();
        if (outline != null)
            outline.enabled = false;
    }

    private System.Collections.IEnumerator DelayedDeactivate()
    {
        yield return new WaitForSeconds(delayBeforeDeactivate);
        objectToDeactivate.SetActive(false);
    }
}
