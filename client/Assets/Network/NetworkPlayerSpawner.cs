using UnityEngine;

public class NetworkPlayerSpawner : MonoBehaviour
{
    private GameManagerScript _gameManager;
    private readonly Vector2[] _instantiatePositions = new Vector2[] {
        new Vector2(540, 400),
        new Vector2(540, 1520),
        new Vector2(320, 400),
        new Vector2(320, 1520),
        new Vector2(760, 400),
        new Vector2(760, 1520),
    };

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

    private GameObject InstantitatePlayer(int queue)
    {
        return GameObject.Instantiate(Resources.Load<GameObject>("player" + (queue + 1)));
    }

    private GameObject SpawnServerPlayer(int deviceId, int queue)
    {
        if (_gameManager.Players.ContainsKey(deviceId))
            return _gameManager.Players[deviceId];
        var go = InstantitatePlayer(queue);
        Vector3 pos = _instantiatePositions[queue % _instantiatePositions.Length];
        pos.z = -1;
        go.transform.position = pos;
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
        var go = InstantitatePlayer(playerState.Queue);
        go.transform.position = playerState.Position;
        var queue = go.AddComponent<PlayerQueue>();
        queue.Value = playerState.Queue;
        if (ownerDeviceId == WSConfig.DeviceId)
            go.AddComponent<ClientPlayerInputHandler>();
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
        myPlayer.transform.position = _instantiatePositions[0];
        if (!myPlayer.GetComponent<ClientPlayerInputHandler>())
            myPlayer.AddComponent<ClientPlayerInputHandler>();

        if (!myPlayer.GetComponent<ServerPlayerSimulator>())
        {
            var serverPlayer = myPlayer.AddComponent<ServerPlayerSimulator>();
            serverPlayer.DeviceId = WSConfig.DeviceId;
        }
    }
}