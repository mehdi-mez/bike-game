using UnityEngine;

public class Pedestal : MonoBehaviour
{
    public int index;
    public PedestalPuzzleManager puzzleManager;
    public GameObject activeVisual;
    private bool isActive = false;

    public void OnInteract()
    {
        puzzleManager.ToggleSkull(index);
    }

    public void Activate()
    {
        isActive = true;
        if (activeVisual != null)
            activeVisual.SetActive(true);
    }

    public void Deactivate()
    {
        isActive = false;
        if (activeVisual != null)
            activeVisual.SetActive(false);
    }

    public void ResetState()
    {
        isActive = false;
        if (activeVisual != null)
            activeVisual.SetActive(false);
    }
}
