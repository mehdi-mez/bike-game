using UnityEngine;
using UnityEngine.Events;

public class PedestalPuzzleManager : MonoBehaviour
{
    [Tooltip("Assign 5 pedestal objects in order.")]
    public Pedestal[] pedestals;
    [Tooltip("Object to activate when puzzle is solved")]
    public GameObject rewardObject;
    
    public UnityEvent onPuzzleSolved;
    
    private bool[] skullPlaced = new bool[5];
    private bool isSolved = false;

    public void ToggleSkull(int index)
    {
        if (isSolved) return; // Don't allow changes after solving
        
        // Toggle the clicked pedestal and its immediate neighbors
        ToggleSinglePedestal(index);
        
        // Toggle left neighbor if exists
        if (index > 0)
        {
            ToggleSinglePedestal(index - 1);
        }
        
        // Toggle right neighbor if exists
        if (index < pedestals.Length - 1)
        {
            ToggleSinglePedestal(index + 1);
        }
        
        // Check win condition after each interaction
        CheckWinCondition();
    }

    private void ToggleSinglePedestal(int index)
    {
        skullPlaced[index] = !skullPlaced[index];
        
        if (skullPlaced[index])
        {
            pedestals[index].Activate();
        }
        else
        {
            pedestals[index].Deactivate();
        }
    }

    private void CheckWinCondition()
    {
        // Check if all pedestals are active
        foreach (bool placed in skullPlaced)
        {
            if (!placed) return;
        }
        
        // Puzzle solved!
        isSolved = true;
        ActivateReward();
        onPuzzleSolved.Invoke();
        Debug.Log("Puzzle Solved!");
    }

    private void ActivateReward()
    {
        if (rewardObject != null)
        {
            rewardObject.SetActive(true);
            
            // If reward has same visual system as pedestals
            Pedestal rewardPedestal = rewardObject.GetComponent<Pedestal>();
            if (rewardPedestal != null)
            {
                rewardPedestal.Activate();
            }
        }
    }

    public void ResetPuzzle()
    {
        isSolved = false;
        for (int i = 0; i < pedestals.Length; i++)
        {
            skullPlaced[i] = false;
            pedestals[i].ResetState();
        }
        
    }


}