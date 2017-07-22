using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Init : MonoBehaviour
{
    public static bool DebugStandalone = true;

#if !UNITY_EDITOR && UNITY_STANDALONE_OSX
    public static bool IsClient = false;
#else
    public static bool IsClient = true;
#endif

    void Awake()
    {
        InitNetwork();
    }

    void InitNetwork()
    {
        WSServer.Stop();
        WSClient.Disconnect();
        WSServerState.Reset();
        WSClientState.Reset();

        if (DebugStandalone)
        {
            WSServer.StartFake();
            WSClient.ConnectFake();
        }
        else if (IsClient)
        {
            WSClient.Connect();
            WSClient.Join();
        }
        else
        {
            WSServer.Start();
        }
    }

    void OnApplicationQuit()
    {
        if (WSServer.IsRunning)
            WSServer.Stop();
        if (WSClient.IsConnected)
            WSClient.Disconnect();
    }
}
