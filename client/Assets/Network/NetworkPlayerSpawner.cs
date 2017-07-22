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
        if (WSServer.IsRunning && !Init.DebugStandalone)
            UpdateOnServer();
        if (!WSServer.IsRunning && !Init.DebugStandalone)
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
        var team = queue % 2;
        var netManager = _gameManager.GetComponent<ServerNetManager>();
        serverPlayer.OnEatSmallNetItem += playerDeviceId => netManager.SmallNet(team);
        serverPlayer.OnEatBigNetItem += playerDeviceId => netManager.BigNet(team);
        var playerQueue = go.AddComponent<PlayerQueue>();
        playerQueue.Value = queue;
        go.GetComponent<PlayerScript>().PlayerHPValue = 100;
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
        Vector3 pos = playerState.Position;
        pos.z = -1;
        go.transform.position = pos;
        go.GetComponent<Rigidbody2D>().velocity = playerState.Velocity;
        go.GetComponent<PlayerScript>().PlayerHPValue = playerState.Hp;
        var queue = go.AddComponent<PlayerQueue>();
        queue.Value = playerState.Queue;
        if (ownerDeviceId == WSConfig.DeviceId)
        {
            go.AddComponent<ClientPlayerInputHandler>();
            // TODO: 옮길것.
            var team = playerState.Queue % 2;
            var angleZ = team == 0 ? 0 : 180;
            var cam = GameObject.Find("Main Camera");
            if (cam == null) Debug.LogError("cam is null");
            else cam.transform.localEulerAngles = new Vector3(0, 0, angleZ);
        }
        _gameManager.Players[ownerDeviceId] = go;
        return go;
    }

    private void SetMyPlayerForDebug()
    {
        var myPlayerExists = _gameManager.Players.ContainsKey(WSConfig.DeviceId);
        if (myPlayerExists) return;

        var myPlayer = SpawnClientPlayer(new PlayerState()
        {
            DeviceId = WSConfig.DeviceId,
            Position = _instantiatePositions[0],
            Velocity = Vector2.zero,
            Queue = 0,
            Hp = 100,
        });

        if (!myPlayer.GetComponent<ClientPlayerInputHandler>())
            myPlayer.AddComponent<ClientPlayerInputHandler>();

        if (!myPlayer.GetComponent<ServerPlayerSimulator>())
        {
            var serverPlayer = myPlayer.AddComponent<ServerPlayerSimulator>();
            serverPlayer.DeviceId = WSConfig.DeviceId;
            var netManager = _gameManager.GetComponent<ServerNetManager>();
            serverPlayer.OnEatSmallNetItem += playerDeviceId => netManager.SmallNet(0);
            serverPlayer.OnEatBigNetItem += playerDeviceId => netManager.BigNet(0);
        }
    }
}