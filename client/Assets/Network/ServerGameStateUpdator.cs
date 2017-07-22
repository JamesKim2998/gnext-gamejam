﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerGameStateUpdator : MonoBehaviour
{
    private GameManagerScript _gameManager;

    void Awake()
    {
        _gameManager = GetComponent<GameManagerScript>();
    }

    void Update()
    {
        if (_gameManager == null) return;

        var gameState = WSServerState.GameState;

        // ball
        gameState.BallPosition = _gameManager.Ball.transform.position;
        gameState.BallVelocity = _gameManager.Ball.GetComponent<Rigidbody2D>().velocity;

        // players
        var clientPlayers = _gameManager.Players;
        gameState.PlayersState.Clear();
        foreach (var kv in clientPlayers)
        {
            gameState.PlayersState.Add(new PlayerState
            {
                DeviceId = kv.Key,
                Queue = kv.Value.GetComponent<PlayerQueue>().Value,
                Position = kv.Value.transform.position,
            });
        }
    }
}
