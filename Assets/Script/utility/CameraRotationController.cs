using UnityEngine;

public class CameraRotationController : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float rotationSpeed = 50f;

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); // A/D or Left/Right
        float verticalInput = Input.GetAxis("Vertical");     // W/S or Up/Down

        
        // Horizontal: rotate around Y-axis (yaw)
        transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime, Space.World);

        // Vertical: rotate around local X-axis (pitch)
        transform.Rotate(Vector3.right, -verticalInput * rotationSpeed * Time.deltaTime, Space.Self);
    }
}
