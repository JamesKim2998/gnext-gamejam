using System.Collections.Generic;
using UnityEngine;

public class GameNetworkManager : MonoBehaviour
{
    private GameManagerScript _gameManager;

    private void Awake()
    {
        _gameManager = GetComponent<GameManagerScript>();
        if (WSServer.IsRunning)
            gameObject.AddComponent<ServerGameStateUpdator>();
        if (!WSServer.IsRunning)
            gameObject.AddComponent<ClientGameStateLoader>();
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
        foreach (var deviceId in WSServerState.JoinedPlayers)
        {
            if (_gameManager.Players.ContainsKey(deviceId))
                continue;
            _gameManager.Players[deviceId] = SpawnServerPlayer(deviceId);
        }
    }

    private GameObject SpawnServerPlayer(int deviceId)
    {
        var go = GameObject.Instantiate(Resources.Load<GameObject>("enemy"));
        var serverPlayer = go.AddComponent<ServerPlayerSimulator>();
        serverPlayer.DeviceId = deviceId;
        return go;
    }

    private void UpdateOnClient()
    {
        var myDeviceId = WSConfig.DeviceId;
        var myPlayerExists = _gameManager.Players.ContainsKey(myDeviceId);
        if (!myPlayerExists)
        {
            _gameManager.Players[myDeviceId] = SpawnClientPlayer(myDeviceId);
        }

        foreach (var playerInput in WSClientState.GameState.PlayersState)
        {
            var ownerDeviceId = playerInput.DeviceId;
            if (_gameManager.Players.ContainsKey(ownerDeviceId))
                continue;
            _gameManager.Players[ownerDeviceId] = SpawnClientPlayer(ownerDeviceId);
        }
    }

    private GameObject SpawnClientPlayer(int ownerDeviceId)
    {
        var go = GameObject.Instantiate(Resources.Load<GameObject>("enemy"));
        if (ownerDeviceId == WSConfig.DeviceId)
            go.AddComponent<ClientPlayerInputHandler>();
        return go;
    }

    private void SetMyPlayerForDebug()
    {
        var myPlayerExists = _gameManager.Players.ContainsKey(WSConfig.DeviceId);
        if (myPlayerExists)
        {
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
}