using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MapManager
{
    public static event Action<bool> OnClickLocation;
    public static event Action ShowMap;
    public static event Action HideMap;

    public static void InvokeShowMap()
    {
        ShowMap?.Invoke();
    }

    public static void InvokeOnClickLocation(bool value)
    {
        OnClickLocation?.Invoke(value);
        HideMap?.Invoke();
    }

    public static void InvokeHideMap()
    {
        HideMap?.Invoke();
    }
}
