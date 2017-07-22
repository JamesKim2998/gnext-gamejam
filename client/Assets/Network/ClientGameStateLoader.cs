using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientGameStateLoader : MonoBehaviour
{
    public GameManagerScript _gameManager;
    public int _lastFrameCount = 0;

    void Awake()
    {
        _gameManager = GetComponent<GameManagerScript>();
    }

    void Update()
    {
        if (_gameManager == null) return;

        WSClient.GetGameState();

        var frameDelta = WSClientState.GameState.FrameCount - _lastFrameCount;
        if (frameDelta <= 0) return;
        WSClientState.GameState.FrameCount = _lastFrameCount;

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
                LoadPlayer(clientPlayers[serverPlayer.DeviceId], serverPlayer, frameDelta);
            }
        }
    }

    public static void LoadPlayer(GameObject player, PlayerState playerState, int frameDelta)
    {
        Vector3 targetPosition = playerState.Position;
        targetPosition.z = -1;
        var orgPosition = player.transform.position;
        var newPosition = Vector3.Lerp(orgPosition, targetPosition, frameDelta / 6f);
        player.transform.position = newPosition;

        player.GetComponent<Rigidbody2D>().velocity = playerState.Velocity;
    }
}
