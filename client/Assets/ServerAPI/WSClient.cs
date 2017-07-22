using UnityEngine;
using WebSocketSharp;

public static class WSClientState
{
    public static GameState GameState = new GameState();
}

public static class WSClient
{
    private static WebSocket _join;
    private static WebSocket _updatePlayerInput;
    private static WebSocket _getGameState;
    private static WebSocket _broadcast;

    private static bool _debugStandaloneIsConnected = false;

    public static bool IsConnected
    {
        get
        {
            if (Init.DebugStandalone) return _debugStandaloneIsConnected;
            return _join != null || _updatePlayerInput != null || _getGameState != null;
        }
    }

    public static void Connect()
    {
        if (Init.DebugStandalone)
        {
            _debugStandaloneIsConnected = true;
            return;
        }

        if (_join != null)
            _join.Close();
        if (_updatePlayerInput != null)
            _updatePlayerInput.Close();
        if (_getGameState != null)
            _getGameState.Close();
        if (_broadcast != null)
            _broadcast.Close();

        var serverAddr = WSConfig.ServerAddr;
        _join = new WebSocket(serverAddr + "Join");
        _join.Connect();

        _updatePlayerInput = new WebSocket(serverAddr + "UpdatePlayerInput");
        _updatePlayerInput.Connect();

        _getGameState = new WebSocket(serverAddr + "GetGameState");
        _getGameState.OnMessage += (sender, e) =>
        {
            // Debug.Log("GameState: " + e.Data);
            WSClientState.GameState = JsonUtility.FromJson<GameState>(e.Data);
        };
        _getGameState.Connect();

        _broadcast = new WebSocket(serverAddr + "Broadcast");
        _broadcast.OnMessage += (sender, e) =>
        {
            Debug.Log("Broadcast: " + e.Data);
            var protocolEnd = e.Data.IndexOf('{');
            var protocol = e.Data.Substring(0, protocolEnd);
            var json = e.Data.Substring(protocolEnd);
            HandleBroadcast(protocol, json);
        };
        _broadcast.Connect();
    }

    public static void Disconnect()
    {
        if (_debugStandaloneIsConnected)
        {
            _debugStandaloneIsConnected = false;
            return;
        }

        if (_join != null)
        {
            _join.Close();
            _join = null;
        }

        if (_updatePlayerInput != null)
        {
            _updatePlayerInput.Close();
            _updatePlayerInput = null;
        }

        if (_getGameState != null)
        {
            _getGameState.Close();
            _getGameState = null;
        }

        if (_broadcast != null)
        {
            _broadcast.Close();
            _broadcast = null;
        }
    }

    public static void Join()
    {
        if (Init.DebugStandalone)
        {
            WSServerState.AddJoinPlayer(WSConfig.DeviceId);
            return;
        }

        if (_join == null)
        {
            Debug.LogError("Not yet connected");
            return;
        }

        _join.Send(WSConfig.DeviceId.ToString());
    }

    public static void UpdatePlayerInput(PlayerInput playerInput)
    {
        if (Init.DebugStandalone)
        {
            WSServerState.SetPlayerInput(playerInput);
            return;
        }

        if (_updatePlayerInput == null)
        {
            Debug.LogError("Not yet connected");
            return;
        }

        var json = JsonUtility.ToJson(playerInput);
        _updatePlayerInput.Send(json);
    }

    public static void GetGameState()
    {
        if (Init.DebugStandalone)
        {
            WSClientState.GameState = WSServerState.GameState.Clone();
            return;
        }

        if (_getGameState == null)
        {
            Debug.LogError("Not yet connected");
            return;
        }

        _getGameState.Send("GetGameState");
    }

    public static void HandleBroadcast(string protocol, string json)
    {
        if (protocol == "SpawnItem")
        {
            var serDe = JsonUtility.FromJson<SerDeSpawnItem>(json);
            if (WSClientBroadcast.SpawnItem != null)
                WSClientBroadcast.SpawnItem(serDe);
        }
        else if (protocol == "DestroyItem")
        {
            var serDe = JsonUtility.FromJson<SerDeDestroyItem>(json);
            if (WSClientBroadcast.DestroyItem != null)
                WSClientBroadcast.DestroyItem(serDe);
        }
        else
        {
            Debug.LogError("Unhandled protocol: " + protocol);
        }
    }
}
