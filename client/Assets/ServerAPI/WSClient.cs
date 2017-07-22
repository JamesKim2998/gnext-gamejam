using UnityEngine;
using WebSocketSharp;

public static class WSClientState
{
    public static GameState GameState;
}

public static class WSClient
{
    private static string _serverAddr;
    public static string ServerAddr
    {
        get
        {
            if (string.IsNullOrEmpty(_serverAddr))
            {
                var key = "WSClient_ServerAddr";
                _serverAddr = PlayerPrefs.GetString(key, "ws://172.17.192.234:8080/");
            }
            return _serverAddr;
        }
        set
        {
            var key = "WSClient_ServerAddr";
            _serverAddr = value;
            PlayerPrefs.SetString(key, value);
        }
    }
    private static WebSocket _join;
    private static WebSocket _updatePlayerInput;
    private static WebSocket _getGameState;

    public static void Connect()
    {
        if (_join != null)
            _join.Close();
        if (_updatePlayerInput != null)
            _updatePlayerInput.Close();
        if (_getGameState != null)
            _getGameState.Close();

        _join = new WebSocket(ServerAddr + "Join");
        _join.Connect();

        _updatePlayerInput = new WebSocket(ServerAddr + "UpdatePlayerInput");
        _updatePlayerInput.Connect();

        _getGameState = new WebSocket(ServerAddr + "GetGameState");
        _getGameState.OnMessage += (sender, e) =>
        {
            // Debug.Log("GameState: " + e.Data);
            WSClientState.GameState = JsonUtility.FromJson<GameState>(e.Data);
        };
        _getGameState.Connect();
    }

    public static void Join()
    {
        _join.Send(WSConfig.DeviceId.ToString());
    }

    public static void UpdatePlayerInput(PlayerInput playerInput)
    {
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
        _getGameState.Send("GetGameState");
    }
}