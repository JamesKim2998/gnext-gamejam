using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreView : MonoBehaviour
{
    public SpriteRenderer Score1;
    public SpriteRenderer Score2;
    public Sprite[] NumSprite = new Sprite[10];

    void Start()
    {
        Score1.GetComponent<SpriteRenderer>().sprite = NumSprite[0];
        Score2.GetComponent<SpriteRenderer>().sprite = NumSprite[0];
    }

    public void Set(int p1Score, int p2Score)
    {
        Score1.sprite = NumSprite[p1Score % 10];
        Score2.sprite = NumSprite[p2Score % 10];
    }
}
