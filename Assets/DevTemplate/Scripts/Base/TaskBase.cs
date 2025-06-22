using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskBase : MonoBehaviour
{

    public int TaskNo = -1;
    public Action AC_OnTaskFinished;
    public bool b_TaskDone = false;

    [HideInInspector] public TaskManagerBase taskManager;

    public void ActivateTask(TaskManagerBase taskManagerBase)
    {
        taskManager = taskManagerBase;
        OnTaskActivate();
    }
    public virtual void OnTaskActivate()
    {
        
    }

    public virtual void OnTaskDeactivate()
    {

    }

    public void TaskComplete()
    {
        AC_OnTaskFinished?.Invoke();
    }

    public void TaskDeactive()
    {
        if(!b_TaskDone)OnTaskDeactivate();
        b_TaskDone = true;
    }

    
}
