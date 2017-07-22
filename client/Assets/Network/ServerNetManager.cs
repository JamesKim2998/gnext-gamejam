using UnityEngine;

public class ServerNetManager : MonoBehaviour
{
    private GameManagerScript _gameManager;

    private void Awake()
    {
        _gameManager = GetComponent<GameManagerScript>();
    }

    public void SmallNet(int team)
    {
        // Debug.Log("SmallNet: " + team);
        _gameManager.NetManager.SmallNet(team);
        WSServer.SmallNet(team);
    }

    public void BigNet(int team)
    {
        // Debug.Log("BigNet: " + team);
        _gameManager.NetManager.BigNet(team);
        WSServer.BigNet(team);
    }
}
