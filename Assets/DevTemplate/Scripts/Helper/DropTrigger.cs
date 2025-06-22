using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTrigger : InteractBase
{
    [SerializeField] E_hand discardingHand;
    
    public override void OnInteract()
    {
        PickupManager.Instance.DropObject(discardingHand);
        
    }

    public void Highlight(bool show)
    {

    }
}
