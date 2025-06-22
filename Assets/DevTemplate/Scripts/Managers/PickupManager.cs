using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoSingleton<PickupManager>
{
    [Header("Handpos")]
    [SerializeField] Transform leftHandPos;
    [SerializeField] Transform rightHandPos;


    public PickupBase leftGrabedObject;
    public PickupBase rightGrabedObject;


    void Start()
    {
        EventManager.AC_ObjectDropped += ObjetcDropped;
    }

    public bool PickObject(PickupBase pickupBase)
    {
        if (leftGrabedObject && rightGrabedObject) return false;

        if (!rightGrabedObject && pickupBase.canPickablebyRight)
        {
            rightGrabedObject = pickupBase;
            pickupBase.Picked(rightHandPos);

            return true;
        }
        else if (!leftGrabedObject && pickupBase.canPickablebyLeft)
        {
            leftGrabedObject = pickupBase;
            pickupBase.Picked(leftHandPos);

            return true;
        }








        return false;
    }

    void ObjetcDropped(PickupBase pickupBase)
    {
        if (leftGrabedObject == pickupBase)
        {
            leftGrabedObject = null;
        }
        else if (rightGrabedObject == pickupBase)
        {
            rightGrabedObject = null;
        }
    }

    public bool DropObject(E_hand e_Hand)
    {
        switch (e_Hand)
        {
            case E_hand.Lefthand:
                if (leftGrabedObject)
                {
                    leftGrabedObject.Drop();
                    leftGrabedObject = null;
                    return true;
                }
                else return false;
            case E_hand.RightHand:
                if (rightGrabedObject)
                {
                    rightGrabedObject.Drop();
                    rightGrabedObject = null;
                    return true;
                }
                else return false;
            case E_hand.AnyHand:
                bool dropStatus = false;
                if (leftGrabedObject)
                {
                    leftGrabedObject.Drop();
                    leftGrabedObject = null;
                    dropStatus = true;
                }
                if (rightGrabedObject)
                {
                    rightGrabedObject.Drop();
                    rightGrabedObject = null;
                    dropStatus = true;
                }

                return dropStatus;
            default: return false;
        }
    }

    public bool DropObject(PickupBase pickupBase)
    {
        if (leftGrabedObject && leftGrabedObject == pickupBase)
        {
            leftGrabedObject.Drop();
            leftGrabedObject = null;
            return true;
        }
        else if (rightGrabedObject && rightGrabedObject == pickupBase)
        {
            rightGrabedObject.Drop();
            rightGrabedObject = null;
            return true;
        }

        return false;
    }
}
