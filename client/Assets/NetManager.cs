using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetManager : MonoBehaviour
{
    GameObject nets;
    GameObject nets2;

    public Sprite DS;
    public Sprite DM;
    public Sprite DL;
    public Sprite US;
    public Sprite UM;
    public Sprite UL;

    void Awake()
    {
        nets = GameObject.Find("net");
        nets2 = GameObject.Find("net2");
    }

    IEnumerator SmallNet()
    {
        if (GameManagerScript.firstturn)
        {
            nets = GameObject.Find("net");
            nets.GetComponent<SpriteRenderer>().sprite = DS;
            nets.GetComponent<BoxCollider2D>().size = new Vector2(0.6f, 0.18f);
            yield return new WaitForSeconds(5.0f);
            nets.GetComponent<SpriteRenderer>().sprite = DM;
            nets.GetComponent<BoxCollider2D>().size = new Vector2(0.8f, 0.18f);
        }
        else
        {
            nets2 = GameObject.Find("net2");
            nets2.GetComponent<SpriteRenderer>().sprite = US;
            nets2.GetComponent<BoxCollider2D>().size = new Vector2(0.6f, 0.18f);
            yield return new WaitForSeconds(5.0f);
            nets2.GetComponent<SpriteRenderer>().sprite = UM;
            nets2.GetComponent<BoxCollider2D>().size = new Vector2(0.8f, 0.18f);
        }
    }

    IEnumerator BigNet()
    {
        if (GameManagerScript.firstturn)
        {
            nets2 = GameObject.Find("net2");
            nets2.GetComponent<SpriteRenderer>().sprite = UL;
            nets2.GetComponent<BoxCollider2D>().size = new Vector2(1.1f, 0.18f);
            yield return new WaitForSeconds(5.0f);
            nets2.GetComponent<SpriteRenderer>().sprite = UM;
            nets2.GetComponent<BoxCollider2D>().size = new Vector2(0.8f, 0.18f);
        }
        else
        {
            nets = GameObject.Find("net");
            nets.GetComponent<SpriteRenderer>().sprite = DL;
            nets.GetComponent<BoxCollider2D>().size = new Vector2(1.1f, 0.18f);
            yield return new WaitForSeconds(5.0f);
            nets.GetComponent<SpriteRenderer>().sprite = DM;
            nets.GetComponent<BoxCollider2D>().size = new Vector2(0.8f, 0.18f);
        }
    }
}
