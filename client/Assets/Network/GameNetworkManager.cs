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

        if (WSServer.IsRunning || Init.DebugStandalone)
        {
            var gameManager = GetComponent<GameManagerScript>();
            var scoreManager = gameObject.AddComponent<ServerScoreManager>();
            gameManager.Score = scoreManager;
            var ballSimulator = gameManager.Ball.AddComponent<ServerBallSimultor>();
            ballSimulator.OnP1Score += scoreManager.P1ScorePlus;
            ballSimulator.OnP2Score += scoreManager.P2ScorePlus;
        }
        else
        {
            var gameManager = GetComponent<GameManagerScript>();
            var scoreManager = gameObject.AddComponent<ClientScoreManager>();
            gameManager.Score = scoreManager;
        }

        if (WSServer.IsRunning || Init.DebugStandalone)
        {
            gameObject.AddComponent<ServerNetManager>();
        }
        else
        {
            gameObject.AddComponent<ClientNetManager>();
        }
    }

    private void Update()
    {
        WSClient.HandleAllBroadcast();
    }
}
