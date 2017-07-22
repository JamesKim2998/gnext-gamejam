using System.Collections.Generic;
using UnityEngine;

public class GameNetworkManager : MonoBehaviour
{
    private bool _didRegisteredPlayerBoost;

    private void Awake()
    {
        var gameManager = GetComponent<GameManagerScript>();
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
            var scoreManager = gameObject.AddComponent<ServerScoreManager>();
            gameManager.Score = scoreManager;
            var ballSimulator = gameManager.Ball.AddComponent<ServerBallSimultor>();
            ballSimulator.OnP1Score += scoreManager.P1ScorePlus;
            ballSimulator.OnP2Score += scoreManager.P2ScorePlus;
        }
        else
        {
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

        if (WSClient.IsConnected && !Init.DebugStandalone)
        {
            _didRegisteredPlayerBoost = true;
            WSClientBroadcast.PlayerPowerUp += OnPlayerPowerUp;
            WSClientBroadcast.PlayerGroggy += OnPlayerGroggy;
        }
    }

    private void OnDestroy()
    {
        if (_didRegisteredPlayerBoost)
        {
            WSClientBroadcast.PlayerPowerUp -= OnPlayerPowerUp;
            WSClientBroadcast.PlayerGroggy -= OnPlayerGroggy;
            _didRegisteredPlayerBoost = false;
        }
    }

    private void Update()
    {
        WSClient.HandleAllBroadcast();
    }

    private void OnPlayerPowerUp(int deviceId)
    {
        var gameManager = GetComponent<GameManagerScript>();
        if (!gameManager.Players.ContainsKey(deviceId)) return;
        var player = gameManager.Players[deviceId];
        player.GetComponent<PlayerScript>().PowerTimer();
    }

    private void OnPlayerGroggy(int deviceId)
    {
        var gameManager = GetComponent<GameManagerScript>();
        if (!gameManager.Players.ContainsKey(deviceId)) return;
        var player = gameManager.Players[deviceId];
        player.GetComponent<PlayerScript>().GroggyTimer();
    }
}
