using System;

public static class WSClientBroadcast
{
    public static System.Action<SerDeSpawnItem> SpawnItem;
    public static System.Action<SerDeDestroyItem> DestroyItem;
    public static System.Action<SerDeScore> ChangeScore;
    public static System.Action<int> SmallNet;
    public static System.Action<int> BigNet;
    public static System.Action<int> PlayerPowerUp;
    public static System.Action<int> PlayerGroggy;
}