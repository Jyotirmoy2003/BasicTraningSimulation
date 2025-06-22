using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class T_GrabObject_Tutorial : TaskBase
{
    [SerializeField] PickupBase pickupGO;


    public override void OnTaskActivate()
    {
        base.OnTaskActivate();

        pickupGO.HighlightPickup(true);
        taskManager.SetTaskUidatabyIndex(TaskNo);
        taskManager.guide.AC_OnAudioFinished += AudioFinsihed;
    }

    public override void OnTaskDeactivate()
    {
        pickupGO.HighlightPickup(false);
        TaskComplete();
    }

    void AudioFinsihed()
    {
        taskManager.guide.AC_OnAudioFinished -= AudioFinsihed;
        taskManager.SetTaskUidatabyIndex(TaskNo + 1);
        pickupGO.AC_OnObjectGrabbed += Picked;
        pickupGO.canPick = true;

    }

    void Picked()
    {
        pickupGO.AC_OnObjectGrabbed -= Picked;
        TaskDeactive();
    }
}
