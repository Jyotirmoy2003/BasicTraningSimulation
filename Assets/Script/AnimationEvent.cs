using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    public static Action OnPlayerDoneReload;
   public void OnReloadDone()
   {
        OnPlayerDoneReload?.Invoke();
   }
}
