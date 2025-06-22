using UnityEngine;

public class PlayerInteractionManager : MonoBehaviour
{
    public Camera mainCamera;
    public float interactionDistance = 5f;
    [SerializeField] LayerMask ignoreLayers;

    private IInteractable currentInteractable;

    void Update()
    {
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance,~ignoreLayers))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                if (interactable != currentInteractable)
                {
                    if (currentInteractable != null)
                        currentInteractable.OnPointerExit();

                    currentInteractable = interactable;
                    currentInteractable.OnPointerEnter();
                }
            }
            else
            {
                if (currentInteractable != null)
                {
                    currentInteractable.OnPointerExit();
                    currentInteractable = null;
                }
            }
        }
        else
        {
            if (currentInteractable != null)
            {
                currentInteractable.OnPointerExit();
                currentInteractable = null;
            }
        }
    }
}
