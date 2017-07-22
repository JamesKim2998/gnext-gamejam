using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientGameStateLoader : MonoBehaviour
{
    public GameManagerScript _gameManager;

    void Awake()
    {
        _gameManager = GetComponent<GameManagerScript>();
    }

    void Update()
    {
        if (_gameManager == null) return;

        WSClient.GetGameState();

        // ball
        var gameState = WSClientState.GameState;
        _gameManager.Ball.transform.position = gameState.BallPosition;

        // players
        var serverPlayers = gameState.PlayersState;
        if (gameState.PlayersState != null)
        {
            var clientPlayers = _gameManager.Players;
            foreach (var serverPlayer in serverPlayers)
            {
                if (!clientPlayers.ContainsKey(serverPlayer.DeviceId))
                    continue;
                var clientPlayer = clientPlayers[serverPlayer.DeviceId];
                clientPlayer.transform.position = serverPlayer.Position;
            }
        }
    }
}
