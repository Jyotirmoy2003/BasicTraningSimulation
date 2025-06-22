using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_SimpleDragAndDrop : TaskBase
{
    public PickupBase GOref;



    public override void OnTaskActivate()
    {
        base.OnTaskActivate();
        if (!GOref.picked) taskManager.SetTaskUidatabyIndex(TaskNo);
        else ObjectPicked();


        GOref.AC_OnObjectGrabbed += ObjectPicked;
        GOref.AC_OnObjectDropped += ObjectDropped;

    }

    public override void OnTaskDeactivate()
    {
        base.OnTaskDeactivate();

    }


    void ObjectPicked()
    {
         
    }

    void ObjectDropped()
    {

    }


}
