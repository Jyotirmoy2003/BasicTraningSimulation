using System.Collections.Generic;
using UnityEngine;

public class Cart : MonoBehaviour
{
    private Vector3 resetLoc;
    [Header("Physics")]
    public Rigidbody cartRigidbody;
    public float pushForce = 10f;
    public int weightAmountPerItem = 10;

    [Header("Weights")]
    public List<GameObject> weightsInCart; // Visual weight objects
    private int currentWeightCount = 0;

    void Start()
    {
        if (cartRigidbody == null)
            cartRigidbody = GetComponent<Rigidbody>();

        cartRigidbody.isKinematic = true;
        resetLoc = transform.position;
        UpdateWeightVisuals();
    }

    [NaughtyAttributes.Button]
    void DebugPush()
    {
        PushCart(pushForce);
    }
    public void PushCart(float force)
    {
        cartRigidbody.isKinematic = false;
        cartRigidbody.AddForce(transform.forward * force, ForceMode.Impulse);
    }

    [NaughtyAttributes.Button]
    public void AddWeight()
    {
        currentWeightCount = Mathf.Clamp(currentWeightCount + 1, 0, weightsInCart.Count);
        cartRigidbody.mass += weightAmountPerItem;
        UpdateWeightVisuals();
    }
    [NaughtyAttributes.Button]
    public void RemoveWeight()
    {
        if (currentWeightCount > 0)
        {
            currentWeightCount--;
            cartRigidbody.mass -= weightAmountPerItem;
            UpdateWeightVisuals();
        }
    }

    private void UpdateWeightVisuals()
    {
        for (int i = 0; i < weightsInCart.Count; i++)
        {
            weightsInCart[i].SetActive(i < currentWeightCount);
        }
    }

    [NaughtyAttributes.Button]
    void ResetToLoc()
    {
        cartRigidbody.isKinematic = true;
        transform.position = resetLoc;
        transform.eulerAngles = Vector3.zero;
        
    }
}
