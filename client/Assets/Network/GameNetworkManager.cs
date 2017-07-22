using System.Collections.Generic;
using UnityEngine;

public class GameNetworkManager : MonoBehaviour
{
    private void Awake()
    {
        gameObject.AddComponent<NetworkPlayerSpawner>();
        if (WSServer.IsRunning)
            gameObject.AddComponent<ServerGameStateUpdator>();
        if (WSServer.IsRunning || Init.DebugStandalone)
            gameObject.AddComponent<ServerItemSpawner>();
        if (!WSServer.IsRunning && !Init.DebugStandalone)
            gameObject.AddComponent<ClientGameStateLoader>();
        if (!WSServer.IsRunning && !Init.DebugStandalone)
            gameObject.AddComponent<ClientItemSpawner>();
    }

    private void Update()
    {
        WSClient.HandleAllBroadcast();
    }
}
