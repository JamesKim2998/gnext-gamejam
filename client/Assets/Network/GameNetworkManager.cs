using System.Collections.Generic;
using UnityEngine;

public class GameNetworkManager : MonoBehaviour
{
    private GameManagerScript _gameManager;

    private void Awake()
    {
        _gameManager = GetComponent<GameManagerScript>();
        gameObject.AddComponent<NetworkPlayerSpawner>();
        if (WSServer.IsRunning)
            gameObject.AddComponent<ServerGameStateUpdator>();
        if (!WSServer.IsRunning)
            gameObject.AddComponent<ClientGameStateLoader>();
    }
}
