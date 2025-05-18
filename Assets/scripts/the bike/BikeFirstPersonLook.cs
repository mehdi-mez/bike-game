using UnityEngine;

public class BikeFirstPersonLook : MonoBehaviour
{
    public float sensitivity = 2f;
    public float minVertical = -45f;
    public float maxVertical = 45f;
    public float minHorizontal = -90f;
    public float maxHorizontal = 90f;

    private float rotationX = 0f; // Yaw (left/right)
    private float rotationY = 0f; // Pitch (up/down)

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Vector3 angles = transform.localEulerAngles;
        rotationX = angles.y;
        rotationY = angles.x;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        rotationX += mouseX;
        rotationY -= mouseY;

        // Clamp both axes
        rotationX = Mathf.Clamp(rotationX, minHorizontal, maxHorizontal);
        rotationY = Mathf.Clamp(rotationY, minVertical, maxVertical);

        transform.localRotation = Quaternion.Euler(rotationY, rotationX, 0);
    }
}
