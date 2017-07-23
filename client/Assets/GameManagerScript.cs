using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    public Dictionary<int, GameObject> Players = new Dictionary<int, GameObject>();

    public int? MyTeam
    {
        get
        {
            GameObject player;
            if (!Players.TryGetValue(WSConfig.DeviceId, out player))
                return null;
            var queue = player.GetComponent<PlayerQueue>().Value;
            return queue % 2;
        }
    }

    [Header("Ball")]
    public GameObject Ball;

    [Header("Item")]
    public GameObject Item2;
    public GameObject Item3;
    public GameObject Item4;
    public GameObject Item5;
    public GameObject Item6;
    public GameObject Item7;
    public GameObject Item8;
    public GameObject Item9;
    public GameObject Item10;

    public float ServerGameTime = 90;
    public IScoreProvider Score = new ZeroScoreProvider();
    public ScoreView ScoreView;
    public TimerView TimerView;
    public NetManager NetManager;
    public static int GameState;
    public static bool ResetServer;

    public GameObject ReadyPanel;
    public GameObject ReadyButton;

    public GameObject ResultPanel;
    public GameObject WinButton;
    public GameObject LoseButton;
    public GameObject DrawButton;

    public GameObject ManWin;
    public GameObject GirlWin;
    public GameObject ManLose;
    public GameObject GirlLose;

    void OnDestroy()
    {
        GameState = 0;
        WSClient.Leave();
    }

    void GameStart()
    {
        GameState = 1;
        ScoreView.Swap = (MyTeam == 1);
    }

    public void OnReady()
    {
        WSClient.Join();
    }

    //판넬 키는 부분
    void Update()
    {
        ScoreView.Set(Score.P1Score, Score.P2Score);
        TimerView.Set((int)ServerGameTime);

        if (GameState == 0)
        {
            if (Players.ContainsKey(WSConfig.DeviceId))
                ReadyButton.SetActive(false);

            if (Init.DebugStandalone)
            {
                GameStart();
            }
            else if (Players.Count >= 2)
            {
                ReadyPanel.SetActive(false);
                Invoke("GameStart", 0.5f);
            }
            else
            {
                ReadyPanel.SetActive(true);
            }
        }
        else if (GameState == 1)
        {
            if (ServerGameTime < 0.0f || Score.P1Score == 10 || Score.P2Score == 10)
            {
                var maybeMyTeam = MyTeam;
                int myTeam = 0;
                if (maybeMyTeam.HasValue)
                    myTeam = maybeMyTeam.Value;
                else
                {
                    Debug.LogError("My player not found");
                    myTeam = 0;
                }

                GameState = 2;
                var meWinState = 0;
                ResultPanel.SetActive(true);
                if (Score.P1Score > Score.P2Score)
                {
                    ManWin.SetActive(true);
                    GirlLose.SetActive(true);
                    meWinState = (myTeam == 0) ? 1 : -1;
                }
                else if (Score.P1Score == Score.P2Score)
                {
                    ManLose.SetActive(true);
                    GirlLose.SetActive(true);
                    meWinState = 0;
                }
                else
                {
                    ManLose.SetActive(true);
                    GirlWin.SetActive(true);
                    meWinState = (myTeam == 0) ? -1 : 1;
                }

                if (meWinState == 1)
                    WinButton.SetActive(true);
                if (meWinState == 0)
                    DrawButton.SetActive(true);
                if (meWinState == -1)
                    LoseButton.SetActive(true);
            }
        }

        if (ResetServer && WSServer.IsRunning)
        {
            ResetServer = false;
            if (WSServerState.JoinedPlayers.Count < 2)
                WSServerState.Reset();
            SceneManager.LoadScene("main");
        }
    }

    public GameObject GetItemPrefab(int itemType)
    {
        switch (itemType)
        {
            case 2: return Item2;
            case 3: return Item3;
            case 4: return Item4;
            case 5: return Item5;
            case 6: return Item6;
            case 7: return Item7;
            case 8: return Item8;
            case 9: return Item9;
            case 10: return Item10;
            default:
                Debug.LogError("Item not found: " + itemType);
                return Item2;
        }
    }
}
