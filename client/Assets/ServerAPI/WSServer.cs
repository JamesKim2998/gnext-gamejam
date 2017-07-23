using System;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using WebSocketSharp.Server;

public static class WSServer
{
    private class Join : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            var deviceId = int.Parse(e.Data);
            Debug.Log("Join: " + deviceId);
            WSServerState.AddJoinPlayer(deviceId);
        }
    }

    private class Leave : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            var deviceId = int.Parse(e.Data);
            Debug.Log("Leave: " + deviceId);
            WSServerState.RemovePlayer(deviceId);
            if (GameManagerScript.GameState == 2)
                GameManagerScript.ResetServer = true;
        }
    }

    private class UpdatePlayerInput : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            // Debug.Log("PlayerInput: " + e.Data);
            var playerInput = JsonUtility.FromJson<PlayerInput>(e.Data);
            WSServerState.SetPlayerInput(playerInput);
        }
    }

    private class GetGameState : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            var gameState = JsonUtility.ToJson(WSServerState.GameState);
            // Debug.Log("GetGameState: " + gameState);
            Send(gameState);
        }
    }

    private class _Broadcast : WebSocketBehavior { }

    private static bool _debugStandaloneIsRunning;
    public static bool IsRunning
    {
        get
        {
            if (_debugStandaloneIsRunning) return true;
            return _sv != null;
        }
    }

    private static WebSocketServer _sv;

    public static void StartFake()
    {
        Stop();
        _debugStandaloneIsRunning = true;
    }

    public static void Start()
    {
        if (_sv != null) Stop();
        _sv = new WebSocketServer("ws://0.0.0.0:8080/");
        _sv.AddWebSocketService<Join>("/Join");
        _sv.AddWebSocketService<Leave>("/Leave");
        _sv.AddWebSocketService<UpdatePlayerInput>("/UpdatePlayerInput");
        _sv.AddWebSocketService<GetGameState>("/GetGameState");
        _sv.AddWebSocketService<_Broadcast>("/Broadcast");
        _sv.Start();
    }

    public static void Stop()
    {
        if (_debugStandaloneIsRunning)
            _debugStandaloneIsRunning = false;

        if (_sv != null)
        {
            _sv.Stop();
            _sv = null;
        }
    }

    private static void Broadcast(string protocol, string json)
    {
        if (_debugStandaloneIsRunning) return;
        if (_sv == null) return;
        WebSocketServiceHost host;
        if (_sv.WebSocketServices.TryGetServiceHost("/Broadcast", out host))
            host.Sessions.Broadcast(protocol + json);
    }

    public static void SpawnItem(SerDeSpawnItem serDe)
    {
        Broadcast("SpawnItem", JsonUtility.ToJson(serDe));
    }

    public static void DestroyItem(int networkId)
    {
        var serDe = new SerDeDestroyItem { NetworkId = networkId };
        Broadcast("DestroyItem", JsonUtility.ToJson(serDe));
    }

    public static void ChangeScore(SerDeScore serDe)
    {
        // Debug.Log("Score: " + serDe.ScoreP1 + " : " + serDe.ScoreP2);
        Broadcast("ChangeScore", JsonUtility.ToJson(serDe));
    }

    public static void BigNet(int team)
    {
        // Debug.Log("BigNet: " + team);
        Broadcast("BigNet", JsonUtility.ToJson(new SerDeTeam { Team = team }));
    }

    public static void SmallNet(int team)
    {
        // Debug.Log("SmallNet: " + team);
        Broadcast("SmallNet", JsonUtility.ToJson(new SerDeTeam { Team = team }));
    }

    public static void PlayerPowerUp(int deviceId)
    {
        // Debug.Log("SmallNet: " + team);
        Broadcast("PlayerPowerUp", JsonUtility.ToJson(new SerDePlayer { DeviceId = deviceId }));
    }

    public static void PlayerGroggy(int deviceId)
    {
        // Debug.Log("SmallNet: " + team);
        Broadcast("PlayerGroggy", JsonUtility.ToJson(new SerDePlayer { DeviceId = deviceId }));
    }
}