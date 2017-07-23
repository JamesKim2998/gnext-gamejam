using UnityEngine;

public static class WSConfig
{
    private static int _deviceId = -1;
    public static int DeviceId
    {
        get
        {
            if (_deviceId == -1)
                _deviceId = SystemInfo.deviceUniqueIdentifier.GetHashCode();
            return _deviceId;
        }
    }

    private static string _serverAddr;
    public static string ServerAddr = "ws://172.17.192.234:8080/";
    /*
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
    */
}