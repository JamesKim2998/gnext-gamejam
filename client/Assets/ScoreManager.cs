using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
    public static int P1Score;
    public static int P2Score;

    public  GameObject Score2;
    public  GameObject Score1;
    static GameObject score2;
    Canvas UICanvas;

    static public Sprite[] NumSprite = new Sprite[10];
    // Use this for initialization
    void Start () {
        P1Score = 0;
        P2Score = 0;

        UICanvas = GameObject.Find("Canvas").GetComponent<Canvas>();

        //score1 = Instantiate(Score1, new Vector3(-450, 850, 0), Quaternion.identity, UICanvas.transform);
       // score2 = Instantiate(Score2, new Vector3(-320, 850, 0), Quaternion.identity, UICanvas.transform);

        Score1.GetComponent<SpriteRenderer>().sprite = NumSprite[P1Score];
        Score2.GetComponent<SpriteRenderer>().sprite = NumSprite[P2Score];
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void P1ScorePlus()
    {
        P1Score += 1;
    }

    public static void P2ScorePlus()
    {
        P2Score += 1;
    }

    public void ScoreChange()
    {
        Score1.GetComponent<SpriteRenderer>().sprite = NumSprite[P1Score];
        Score2.GetComponent<SpriteRenderer>().sprite = NumSprite[P1Score];
    }
}
