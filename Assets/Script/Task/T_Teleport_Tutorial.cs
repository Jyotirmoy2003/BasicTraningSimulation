using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class T_Teleport_Tutorial : TaskBase
{
    [SerializeField] TeleportPoint teleportPoint;
    [SerializeField] Transform guideMovePos;
    public override void OnTaskActivate()
    {
        base.OnTaskActivate();
        taskManager.guide.SetLookAt(teleportPoint.transform);
        taskManager.guide.MoveTo(guideMovePos.position, 3);
        taskManager.guide.AC_ReachedToDest += GuideReached;
    }

    public override void OnTaskDeactivate()
    {
       
        TaskComplete();
    }

    void GuideReached()
    {
        taskManager.guide.AC_ReachedToDest -= GuideReached;
        taskManager.guide.AC_OnAudioFinished += AudioFinshed;
        taskManager.SetTaskUidatabyIndex(TaskNo);
        taskManager.guide.SetLookAt(_GameAssets.Instance.playerTranform);
    }

    void AudioFinshed()
    {
        taskManager.guide.AC_OnAudioFinished -= AudioFinshed;
        taskManager.SetTaskUidatabyIndex(TaskNo + 1);
        //Highlight teleport point

        EventManager.Instance.AC_OnTeleportDone += TeleportDone;
    }

    void TeleportDone(TeleportPoint point)
    {
        EventManager.Instance.AC_OnTeleportDone -= TeleportDone;
        taskManager.guide.MoveBackToOriginalpos();
        TaskDeactive();
    }
}
