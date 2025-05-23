using UnityEngine;

public class BikeMountManager : MonoBehaviour
{
    public GameObject player;
    public Camera playerCamera;
    public Camera bikeCamera;
    public MonoBehaviour bikeController;
    public Transform dismountPoint;

    // This script is on the bike camera
    private BikeFirstPersonLook bikeLook;

    private bool isMounted = false;

    void Start()
    {
        if (bikeCamera != null)
            bikeLook = bikeCamera.GetComponent<BikeFirstPersonLook>();
    }

    void Update()
    {
        // Press Left Control to dismount
        if (isMounted && Input.GetKeyDown(KeyCode.LeftControl))
        {
            DismountBike();
        }
    }

    public void MountBike()
    {
        if (isMounted) return;

        player.SetActive(false);
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

        player.SetActive(true);
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
