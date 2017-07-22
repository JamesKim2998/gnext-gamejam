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

    private class UpdatePlayerInput : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            Debug.Log("PlayerInput: " + e.Data);
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
            Debug.Log("GetGameState");
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