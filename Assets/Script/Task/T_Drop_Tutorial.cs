using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_Drop_Tutorial : TaskBase
{
    [SerializeField] DropTrigger dropTrigger;
    public override void OnTaskActivate()
    {
        base.OnTaskActivate();
        taskManager.SetTaskUidatabyIndex(TaskNo);
        dropTrigger.canInteract = true;
        dropTrigger.Highlight(true);
        EventManager.AC_ObjectDropped += ObjectDropped;
    }

    public override void OnTaskDeactivate()
    {
        base.OnTaskDeactivate();
        TaskComplete();
    }

    void ObjectDropped(PickupBase pickupBase)
    {
        EventManager.AC_ObjectDropped -= ObjectDropped;
        dropTrigger.Highlight(false);
        TaskDeactive();
    }
}
