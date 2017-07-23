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
        Application.targetFrameRate = 60;
        InitNetwork();
    }

    void InitNetwork()
    {
        WSServerState.Reset();

        if (DebugStandalone)
        {
            WSServer.Stop();
            WSServerState.Reset();
            WSClient.Disconnect();
            WSClientState.Reset();
            WSServer.StartFake();
            WSClient.ConnectFake();
        }
        else if (IsClient)
        {
            WSServer.Stop();
            WSServerState.Reset();
            WSClient.Connect();
        }
        else
        {
            WSClient.Disconnect();
            WSClientState.Reset();
            WSServer.Start();
        }
    }

    void OnApplicationQuit()
    {
        WSServer.Stop();
        WSClient.Disconnect();
    }
}
