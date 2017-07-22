using System;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using WebSocketSharp.Server;

public static class WSServerState
{
    public static readonly List<int> JoinedPlayers = new List<int>();
    public static readonly Dictionary<int, PlayerInput> PlayerInputs = new Dictionary<int, PlayerInput>();
    public static readonly GameState GameState = new GameState();
}

public static class WSServer
{
    private class Join : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            WSServerState.JoinedPlayers.Add(int.Parse(e.Data));
        }
    }

    private class UpdatePlayerInput : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            var playerInput = JsonUtility.FromJson<PlayerInput>(e.Data);
            WSServerState.PlayerInputs[playerInput.DeviceId] = playerInput;
        }
    }

    private class GetGameState : WebSocketBehavior
    {
        public GetGameState()
            : base()
        {
            EmitOnPing = true;
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            if (!e.IsPing) return;
            Send(JsonUtility.ToJson(WSServerState.GameState));
        }
    }

    public static bool IsRunning { get { return _sv != null; } }

    private static WebSocketServer _sv;

    public static void Start()
    {
        if (_sv != null) Stop();
        _sv = new WebSocketServer("ws://0.0.0.0:8080");
        _sv.AddWebSocketService<Join>("/Join");
        _sv.AddWebSocketService<UpdatePlayerInput>("/UpdatePlayerInput");
        _sv.AddWebSocketService<GetGameState>("/GetGameState");
        _sv.Start();
    }

    public static void Stop()
    {
        if (_sv == null) return;
        _sv.Stop();
        _sv = null;
    }
}