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
        _gameManager.Ball.GetComponent<Rigidbody2D>().velocity = gameState.BallVelocity;

        // players
        var serverPlayers = gameState.PlayersState;
        if (gameState.PlayersState != null)
        {
            var clientPlayers = _gameManager.Players;
            foreach (var serverPlayer in serverPlayers)
            {
                if (!clientPlayers.ContainsKey(serverPlayer.DeviceId))
                    continue;
                LoadPlayer(clientPlayers[serverPlayer.DeviceId], serverPlayer);
            }
        }
    }

    public static void LoadPlayer(GameObject player, PlayerState playerState)
    {
        var pos = playerState.Position;
        player.transform.position = new Vector3(pos.x, pos.y, -1);
    }
}
