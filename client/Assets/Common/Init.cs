using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Init : MonoBehaviour
{
    void Awake()
    {
        var isClient = true;
#if !UNITY_EDITOR && UNITY_STANDALONE_OSX
        isClient = false;
#endif

        if (WSConfig.DebugStandalone)
        {
            // do nothing
            WSServerState.AddJoinPlayer(WSConfig.DeviceId);
        }
        else
        {
            if (isClient)
            {
                WSClient.Connect();
                WSClient.Join();
            }
            else
            {
                WSServer.Start();
            }
        }
    }
}
