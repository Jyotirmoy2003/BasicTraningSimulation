using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskBase : MonoBehaviour
{

    public int TaskNo = -1;
    public Action AC_OnTaskFinished;
    public Action AC_AllSubtaskFinihsed;
    public bool b_TaskDone = false;
    public List<TaskBase> subtaskList = new List<TaskBase>();


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
        if (!b_TaskDone) OnTaskDeactivate();
        b_TaskDone = true;
    }


    #region  Subtask
    int currentSubtaskIndex = 0;

    public void InitateSubtask(int index)
    {
        if (subtaskList.Count > index)
        {
            subtaskList[index].ActivateTask(taskManager);
        }
    }

    
    public void InitateSubtask()
    {
        if (subtaskList.Count < currentSubtaskIndex)
        {
            subtaskList[currentSubtaskIndex].AC_OnTaskFinished += SubtaskFinished;
            subtaskList[currentSubtaskIndex].ActivateTask(taskManager);
        }
        else
        {
            AC_AllSubtaskFinihsed?.Invoke();
        }
    }

    void SubtaskFinished()
    {
        subtaskList[currentSubtaskIndex].AC_OnTaskFinished -= SubtaskFinished;
        currentSubtaskIndex++;
        InitateSubtask();
    }

    public void ForceEndAllSubtask()
    {
        foreach (TaskBase item in subtaskList)
        {
            item.TaskDeactive();
            item.TaskComplete();
        }


    }



    #endregion


}
