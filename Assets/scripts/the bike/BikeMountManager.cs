using UnityEngine;

public class BikeMountManager : MonoBehaviour
{
    public GameObject player;
    public Camera playerCamera;
    public Camera bikeCamera;
    public MonoBehaviour bikeController;
    public Transform dismountPoint;

    private BikeFirstPersonLook bikeLook;
    private bool isMounted = false;

    void Start()
    {
        if (bikeCamera != null)
            bikeLook = bikeCamera.GetComponent<BikeFirstPersonLook>();
    }

    void Update()
    {
        if (isMounted && Input.GetKeyDown(KeyCode.LeftControl))
        {
            DismountBike();
        }
    }

    public void MountBike()
    {
        if (isMounted) return;

        // Just freeze movement & interaction
        var controller = player.GetComponent<CharacterController>();
        if (controller != null) controller.enabled = false;

        var interaction = player.GetComponent<PlayerInteractions>();
        if (interaction != null) interaction.enabled = false;

        // Optionally hide player visuals
        foreach (var r in player.GetComponentsInChildren<Renderer>())
            r.enabled = false;

        playerCamera.gameObject.SetActive(false);
        bikeCamera.gameObject.SetActive(true);

        if (bikeController != null)
            bikeController.enabled = true;

        if (bikeLook != null)
            bikeLook.enabled = true;

        isMounted = true;
    }

    public void DismountBike()
    {
        if (!isMounted) return;

        player.transform.position = dismountPoint.position;
        player.transform.rotation = dismountPoint.rotation;


        var controller = player.GetComponent<CharacterController>();
        if (controller != null) controller.enabled = true;

        var interaction = player.GetComponent<PlayerInteractions>();
        if (interaction != null) interaction.enabled = true;

        foreach (var r in player.GetComponentsInChildren<Renderer>())
            r.enabled = true;

        playerCamera.gameObject.SetActive(true);
        bikeCamera.gameObject.SetActive(false);

        if (bikeController != null)
            bikeController.enabled = false;

        if (bikeLook != null)
            bikeLook.enabled = false;

        isMounted = false;
    }

    public bool IsMounted()
    {
        return isMounted;
    }
}

