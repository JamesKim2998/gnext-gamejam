using UnityEngine;

public static class WSConfig
{
    public static bool DebugStandalone = false;

    public static int DeviceId
    {
        get
        {
            return SystemInfo.deviceUniqueIdentifier.GetHashCode();
        }
    }
}