using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour {

    public Dictionary<int, GameObject> Players = new Dictionary<int, GameObject>();

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

    float GameTime;
    public IScoreProvider Score = new ZeroScoreProvider();
    public ScoreView ScoreView;
    public NetManager NetManager;
    public static bool firstturn;
    public static int GameState;

    public GameObject WinButton;
    public GameObject LoseButton;
    public GameObject DrawButton;

    public GameObject ManWin;
    public GameObject GirlWin;
    public GameObject ManLose;
    public GameObject GirlLose;
    bool finish;


    // Use this for initialization
    void Start () {
        firstturn = false;
        GameTime = 90.0f;
        Invoke("GameStart", 1.0f);
        finish = false;

    }
	
    void GameStart()
    {
        GameState = 1;
    }
    //판넬 키는 부분
    void Update () {
        ScoreView.Set(Score.P1Score, Score.P2Score);

        GameTime -= Time.deltaTime;
        if (finish == false)
        {
            if (GameTime < 0.0f || Score.P1Score == 10 || Score.P2Score == 10)
            {
                GameObject.Find("Panel").SetActive(true);
                if (Score.P1Score > Score.P2Score)
                {
                    ManWin.SetActive(true);
                    GirlLose.SetActive(true);
                    finish = true;
                }
                else if (Score.P1Score == Score.P2Score)
                {
                    ManLose.SetActive(true);
                    GirlLose.SetActive(true);
                    finish = true;
                }
                else
                {
                    ManLose.SetActive(true);
                    GirlWin.SetActive(true);
                    finish = true;
                }
            }
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
