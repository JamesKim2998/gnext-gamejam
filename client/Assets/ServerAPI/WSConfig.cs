using UnityEngine;

public static class WSConfig
{
    public static int DeviceId
    {
        get
        {
            return SystemInfo.deviceUniqueIdentifier.GetHashCode();
        }
    }

    private static string _serverAddr;
    public static string ServerAddr
    {
        get
        {
            if (string.IsNullOrEmpty(_serverAddr))
            {
                var key = "WSClient_ServerAddr";
                _serverAddr = PlayerPrefs.GetString(key, "ws://172.17.192.234:8080/");
            }
            return _serverAddr;
        }
        set
        {
            var key = "WSClient_ServerAddr";
            _serverAddr = value;
            PlayerPrefs.SetString(key, value);
        }
    }
}