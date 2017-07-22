using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Init : MonoBehaviour
{
    void Awake()
    {
        if (WSConfig.DebugStandalone)
        {
            // do nothing
            WSServerState.AddJoinPlayer(WSConfig.DeviceId);
        }
        else
        {
#if !UNITY_EDITOR && UNITY_STANDALONE_OSX
            WSServer.Start();
#else
            WSClient.Connect();
            WSClient.Join();
#endif
        }
    }
}
