using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_Empty_Audio : TaskBase
{
    [SerializeField] float delayBeforeTaskComplete =2;
    public override void OnTaskActivate()
    {
        base.OnTaskActivate();
        taskManager.guide.AC_OnAudioFinished += AudioFinished;
        taskManager.SetTaskUidatabyIndex(TaskNo);
    }

    public override void OnTaskDeactivate()
    {
        base.OnTaskDeactivate();
        LeanTween.delayedCall(delayBeforeTaskComplete, TaskComplete);
    }


    void AudioFinished()
    {
        taskManager.guide.AC_OnAudioFinished -= AudioFinished;
        TaskDeactive();
    }
}
