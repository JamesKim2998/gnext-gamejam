using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetManager : MonoBehaviour
{
    public GameObject nets;
    public GameObject nets2;

    public Sprite DS;
    public Sprite DM;
    public Sprite DL;
    public Sprite US;
    public Sprite UM;
    public Sprite UL;

    public void SmallNet(int team)
    {
        StartCoroutine(CoSmallNet(team));
    }

    private IEnumerator CoSmallNet(int team)
    {
        if (team == 0)
        {
            nets.GetComponent<SpriteRenderer>().sprite = DS;
            nets.GetComponent<BoxCollider2D>().size = new Vector2(0.6f, 0.18f);
            yield return new WaitForSeconds(5.0f);
            nets.GetComponent<SpriteRenderer>().sprite = DM;
            nets.GetComponent<BoxCollider2D>().size = new Vector2(0.8f, 0.18f);
        }
        else
        {
            nets2.GetComponent<SpriteRenderer>().sprite = US;
            nets2.GetComponent<BoxCollider2D>().size = new Vector2(0.6f, 0.18f);
            yield return new WaitForSeconds(5.0f);
            nets2.GetComponent<SpriteRenderer>().sprite = UM;
            nets2.GetComponent<BoxCollider2D>().size = new Vector2(0.8f, 0.18f);
        }
    }

    public void BigNet(int team)
    {
        StartCoroutine(CoBigNet(team));
    }

    private IEnumerator CoBigNet(int team)
    {
        if (team == 0)
        {
            nets2.GetComponent<SpriteRenderer>().sprite = UL;
            nets2.GetComponent<BoxCollider2D>().size = new Vector2(1.1f, 0.18f);
            yield return new WaitForSeconds(5.0f);
            nets2.GetComponent<SpriteRenderer>().sprite = UM;
            nets2.GetComponent<BoxCollider2D>().size = new Vector2(0.8f, 0.18f);
        }
        else
        {
            nets.GetComponent<SpriteRenderer>().sprite = DL;
            nets.GetComponent<BoxCollider2D>().size = new Vector2(1.1f, 0.18f);
            yield return new WaitForSeconds(5.0f);
            nets.GetComponent<SpriteRenderer>().sprite = DM;
            nets.GetComponent<BoxCollider2D>().size = new Vector2(0.8f, 0.18f);
        }
    }
}
