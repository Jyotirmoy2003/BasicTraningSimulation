using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceTrigger : InteractBase
{
    public Action<PickupBase> AC_TriggerInteracted;
    public E_hand intendedHand;

    

    public void EnableTrigger(bool enable)
    {
        ToggleInteract(enable);
    }

    public override void OnInteract()
    {
        base.OnInteract();
        switch (intendedHand)
        {
            case E_hand.Lefthand:
                if (PickupManager.Instance.leftGrabedObject)
                {
                    AC_TriggerInteracted?.Invoke(PickupManager.Instance.leftGrabedObject);
                }
                break;
            case E_hand.RightHand:
                if (PickupManager.Instance.rightGrabedObject)
                {
                    AC_TriggerInteracted?.Invoke(PickupManager.Instance.rightGrabedObject);
                }
                break;
            case E_hand.AnyHand:
                if (PickupManager.Instance.rightGrabedObject)
                {
                    AC_TriggerInteracted?.Invoke(PickupManager.Instance.rightGrabedObject);
                }
                else if (PickupManager.Instance.leftGrabedObject)
                {
                    AC_TriggerInteracted?.Invoke(PickupManager.Instance.leftGrabedObject);
                }
                break;
        }
    }

    
}
