using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPoint : InteractBase
{
    [SerializeField] Transform playerPoint;
    public override void OnInteract()
    {
        canInteract = false;
        EventManager.Instance.AC_OnTeleportInitate?.Invoke();
        UIManager.Instance.BlackScreenFadeIn();

        Invoke(nameof(Teleport), 1f);
    }

    void Teleport()
    {
        _GameAssets.Instance.playerTranform.position = playerPoint.position;
        UIManager.Instance.BlackScreenFadeOut();
        Invoke(nameof(TeleportDone), 2f);
    }

    void TeleportDone()
    {
        EventManager.Instance.AC_OnTeleportDone?.Invoke(this);
    }

    void ListenToTeleportDoneEvent(TeleportPoint teleportPoint)
    {
        if (teleportPoint == this)
        {
            canInteract = false;
        }
        else
        {
            canInteract = true;
        }
    }

    public override void Init()
    {
        base.Init();
        EventManager.Instance.AC_OnTeleportDone += ListenToTeleportDoneEvent;
    }

   
    
}
