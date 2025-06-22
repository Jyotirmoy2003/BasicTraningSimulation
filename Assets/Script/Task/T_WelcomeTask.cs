using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_WelcomeTask : TaskBase
{
    [SerializeField] AudioClip welcomeAudioClip;

    public override void OnTaskActivate()
    {
        base.OnTaskActivate();

        taskManager.guide.AC_OnAudioFinished += OnAudioFinsihed;
        taskManager.guide.PlayDialouge(welcomeAudioClip);
    }


    public override void OnTaskDeactivate()
    {
        base.OnTaskDeactivate();

        TaskComplete();
    }

    void OnAudioFinsihed()
    {
        taskManager.guide.AC_OnAudioFinished -= OnAudioFinsihed;
        TaskDeactive();
    }




}



