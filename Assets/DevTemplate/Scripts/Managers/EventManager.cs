using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoSingleton<EventManager>
{
    public static Action OnChapterStartEvent;
    public static Action OnChapterEndEvent;
    public Action<TeleportPoint> AC_OnTeleportDone;
    public Action AC_OnTeleportInitate;
    public static Action<PickupBase> AC_ObjectDropped;
    public static Action<int, bool, bool> AC_PlaceItemStatusChanged;
}
