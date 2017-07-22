using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Init : MonoBehaviour
{
#if UNITY_EDITOR
    public static bool DebugStandalone = true;
#else
    public static bool DebugStandalone = false;
#endif
    public static bool IsClient;

    void Awake()
    {
        InitNetwork();
    }

    void InitNetwork()
    {
        if (WSServer.IsRunning && IsClient)
            WSServer.Stop();
        if (WSClient.IsConnected && !IsClient)
            WSClient.Disconnect();

        if (IsClient && !WSClient.IsConnected)
        {
            WSClient.Connect();
            WSClient.Join();
        }

        if (!IsClient && !WSServer.IsRunning)
        {
            WSServer.Start();
        }
    }
}
