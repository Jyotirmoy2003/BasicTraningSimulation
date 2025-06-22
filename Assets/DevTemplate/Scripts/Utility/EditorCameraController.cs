using UnityEngine;

public class EditorCameraController : MonoBehaviour
{
    public float lookSpeed = 2f;
    public float moveSpeed = 5f;

    private bool isControlling = false;
    private float yaw = 0f;
    private float pitch = 0f;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))
        {
            if (!isControlling)
            {
                isControlling = true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }

            float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;

            yaw += mouseX;
            pitch -= mouseY;
            pitch = Mathf.Clamp(pitch, -89f, 89f); // prevent flipping

            transform.rotation = Quaternion.Euler(pitch, yaw, 0f); // Z = 0 locked

            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            float y = 0f;
            if (Input.GetKey(KeyCode.E)) y = 1f;
            else if (Input.GetKey(KeyCode.Q)) y = -1f;

            Vector3 move = transform.right * h + transform.forward * v + transform.up * y;
            transform.position += move * moveSpeed * Time.deltaTime;
        }
        else
        {
            if (isControlling)
            {
                isControlling = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
}
