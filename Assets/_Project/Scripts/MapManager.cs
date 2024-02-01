using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MapManager
{
    public static event Action<bool> OnClickLocation;
    public static event Action ShowMap;

    public static void InvokeShowMap()
    {
        ShowMap?.Invoke();
    }

    public static void InvokeOnClickLocation(bool value)
    {
        OnClickLocation?.Invoke(value);
    }
}
