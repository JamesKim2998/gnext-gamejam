using System;

public static class WSClientBroadcast
{
    public static System.Action<SerDeSpawnItem> SpawnItem;
    public static System.Action<SerDeDestroyItem> DestroyItem;
    public static System.Action<SerDeScore> ChangeScore;
}