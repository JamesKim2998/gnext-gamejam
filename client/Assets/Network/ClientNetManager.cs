using UnityEngine;

public class ClientNetManager : MonoBehaviour
{
    private GameManagerScript _gameManager;

    private void Awake()
    {
        WSClientBroadcast.SmallNet += OnSmallNet;
        WSClientBroadcast.BigNet += OnBigNet;
    }

    private void OnDestroy()
    {
        WSClientBroadcast.SmallNet -= OnSmallNet;
        WSClientBroadcast.BigNet -= OnBigNet;
    }

    private void OnSmallNet(int team)
    {
        _gameManager.NetManager.SmallNet(team);
    }

    private void OnBigNet(int team)
    {
        _gameManager.NetManager.BigNet(team);
    }
}
