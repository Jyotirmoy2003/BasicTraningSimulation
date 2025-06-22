using UnityEngine;

public class PlayerUIInteractionManager : MonoBehaviour
{
    public Camera mainCamera;
    public float rayDistance = 5f;
    [SerializeField] LayerMask ignoreLayers;
    private IUI currentUI;

    void Update()
    {
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance,~ignoreLayers))
        {
            IUI ui = hit.collider.GetComponent<IUI>();
            
            if (ui != null)
            {
                if (ui != currentUI)
                {
                    if (currentUI != null)
                        currentUI.OnPointerExit();

                    currentUI = ui;
                    currentUI.OnPointerEnter();
                }
            }
            else
            {
                if (currentUI != null)
                {
                    currentUI.OnPointerExit();
                    currentUI = null;
                }
            }
        }
        else
        {
            if (currentUI != null)
            {
                currentUI.OnPointerExit();
                currentUI = null;
            }
        }
    }
}
