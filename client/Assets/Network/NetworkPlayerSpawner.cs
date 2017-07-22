using UnityEngine;

public class NetworkPlayerSpawner : MonoBehaviour
{
    private GameManagerScript _gameManager;

    private void Awake()
    {
        _gameManager = GetComponent<GameManagerScript>();
    }

    private void Update()
    {
        if (WSServer.IsRunning || Init.DebugStandalone)
            UpdateOnServer();
        if (!WSServer.IsRunning || Init.DebugStandalone)
            UpdateOnClient();
        if (Init.DebugStandalone)
            SetMyPlayerForDebug();
    }

    private void UpdateOnServer()
    {
        for (var i = 0; i < WSServerState.JoinedPlayers.Count; ++i)
        {
            var deviceId = WSServerState.JoinedPlayers[i];
            SpawnServerPlayer(deviceId, i);
        }
    }

    private GameObject SpawnServerPlayer(int deviceId, int queue)
    {
        if (_gameManager.Players.ContainsKey(deviceId))
            return _gameManager.Players[deviceId];
        var go = GameObject.Instantiate(Resources.Load<GameObject>("enemy"));
        var serverPlayer = go.AddComponent<ServerPlayerSimulator>();
        serverPlayer.DeviceId = deviceId;
        var playerQueue = go.AddComponent<PlayerQueue>();
        playerQueue.Value = queue;
        _gameManager.Players[deviceId] = serverPlayer.gameObject;
        return go;
    }

    private void UpdateOnClient()
    {
        foreach (var playerState in WSClientState.GameState.PlayersState)
            SpawnClientPlayer(playerState);
    }

    private GameObject SpawnClientPlayer(PlayerState playerState)
    {
        var ownerDeviceId = playerState.DeviceId;
        if (_gameManager.Players.ContainsKey(ownerDeviceId))
            return _gameManager.Players[ownerDeviceId];
        var go = GameObject.Instantiate(Resources.Load<GameObject>("enemy"));
        go.transform.position = playerState.Position;
        var queue = go.AddComponent<PlayerQueue>();
        if (ownerDeviceId == WSConfig.DeviceId)
            go.AddComponent<ClientPlayerInputHandler>();
        queue.Value = playerState.Queue;
        _gameManager.Players[ownerDeviceId] = go;
        return go;
    }

    private void SetMyPlayerForDebug()
    {
        var myPlayerExists = _gameManager.Players.ContainsKey(WSConfig.DeviceId);
        if (!myPlayerExists)
        {
            SpawnClientPlayer(new PlayerState()
            {
                DeviceId = WSConfig.DeviceId,
                Position = Vector2.one,
                Queue = 0,
            });
        }

        var myPlayer = _gameManager.Players[WSConfig.DeviceId];
        if (!myPlayer.GetComponent<ClientPlayerInputHandler>())
            myPlayer.AddComponent<ClientPlayerInputHandler>();

        if (!myPlayer.GetComponent<ServerPlayerSimulator>())
        {
            var serverPlayer = myPlayer.AddComponent<ServerPlayerSimulator>();
            serverPlayer.DeviceId = WSConfig.DeviceId;
        }
    }
}