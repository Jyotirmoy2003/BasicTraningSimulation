using UnityEngine;

public class SmoothLookAt : MonoBehaviour
{
    [Tooltip("The Transform the mascot will look at")]
    public Transform target;

    [Tooltip("Speed of the head-turning motion")]
    public float lookSpeed = 2f;

    private Quaternion initialRotation;

    void Start()
    {
        // Store the starting rotation so we can smoothly return if target is null
        initialRotation = transform.rotation;
    }

    void Update()
    {
        Quaternion desiredRotation = initialRotation;

        if (target != null)
        {
            Vector3 direction = target.position - transform.position;
            if (direction.sqrMagnitude > 0.0001f)
            {
                // Calculate the desired look-at rotation
                desiredRotation = Quaternion.LookRotation(direction, Vector3.up);
            }
        }

        // Smoothly interpolate rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * lookSpeed);
    }

    /// <summary>
    /// Call this to update the target for the mascot
    /// </summary>
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    /// <summary>
    /// Call this to make the mascot stop looking and return to original orientation
    /// </summary>
    public void ClearTarget()
    {
        target = null;
    }
}
