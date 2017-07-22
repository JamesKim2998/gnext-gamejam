using UnityEngine;
using WebSocketSharp;

public static class WSClientState
{
    public static GameState GameState;
}

public static class WSClient
{
    private static string _serverAddr = "ws://172.17.192.234:8080/";
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

        _join = new WebSocket(_serverAddr + "Join");
        _join.Connect();

        _updatePlayerInput = new WebSocket(_serverAddr + "UpdatePlayerInput");
        _updatePlayerInput.Connect();

        _getGameState = new WebSocket(_serverAddr + "GetGameState");
        _getGameState.OnMessage += (sender, e) =>
        {
            Debug.Log(e.Data);
            WSClientState.GameState = JsonUtility.FromJson<GameState>(e.Data);
        };
        _getGameState.Connect();
    }

    private static int GetDeviceId()
    {
        return SystemInfo.deviceUniqueIdentifier.GetHashCode();
    }

    public static void Join()
    {
        _join.Send(GetDeviceId().ToString());
    }

    public static void UpdatePlayerInput(PlayerInput playerInput)
    {
        if (_updatePlayerInput == null)
        {
            Debug.LogError("Not yet connected");
            return;
        }

        playerInput.DeviceId = GetDeviceId();
        var json = JsonUtility.ToJson(playerInput);
        _updatePlayerInput.Send(json);
    }

    public static void GetGameState()
    {
        _getGameState.Ping();
    }
}