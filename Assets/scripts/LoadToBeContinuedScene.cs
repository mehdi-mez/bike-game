using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadToBeContinuedScene : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger. Loading 'to be continued' scene...");
            SceneManager.LoadScene("to be continued");
        }
    }
}
