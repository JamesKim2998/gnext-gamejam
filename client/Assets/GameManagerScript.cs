using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {

    [Header("Player")]
    public GameObject Player;
    public Dictionary<int, GameObject> Players = new Dictionary<int, GameObject>();


    [Header("Player")]
    public GameObject Enemy;


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


    int random;
    float timer;
    float GameTime;
    public static bool firstturn;


    // Use this for initialization
    void Start () {
        firstturn = false;
        GameTime = 90.0f;
    }
	
	// Update is called once per frame
	void Update () {
        GameTime -= Time.deltaTime;

        if(GameTime <0.0f)
        {
            GameTime = 90.0f;
        }

        // Debug.Log(GameTime);
        random = Random.Range(0, 100);
        /*Debug.Log(random);
        if(random > 990)
        {
            ItemGenerate(random);
        }*/
        timer += Time.deltaTime;

        if(timer > 10.0f)
        {
            timer = 0.0f;
            ItemGenerate(random);
        }
        
	}

    void ItemGenerate(int rand)
    {
        int ItemNum = rand % 7;
        GameObject blueitem = (GameObject)Instantiate(Item2, new Vector3(Random.Range(100.0f, 980.0f), Random.Range(200.0f, 1720.0f), 0), Quaternion.identity);
        GameObject blueitem2 = (GameObject)Instantiate(Item2, new Vector3(Random.Range(100.0f, 980.0f), Random.Range(200.0f, 1720.0f), 0), Quaternion.identity);

        if (ItemNum == 0)
        {
            GameObject item = (GameObject)Instantiate(Item3, new Vector3(Random.Range(100.0f, 980.0f), Random.Range(200.0f, 1720.0f), 0), Quaternion.identity);
        }
        else if (ItemNum == 1)
        {
            GameObject item = (GameObject)Instantiate(Item4, new Vector3(Random.Range(100.0f, 980.0f), Random.Range(200.0f, 1720.0f), 0), Quaternion.identity);
        }
        else if (ItemNum == 2)
        {
            GameObject item = (GameObject)Instantiate(Item5, new Vector3(Random.Range(100.0f, 980.0f), Random.Range(200.0f, 1720.0f), 0), Quaternion.identity);
        }
        else if (ItemNum == 3)
        {
            GameObject item = (GameObject)Instantiate(Item6, new Vector3(Random.Range(100.0f, 980.0f), Random.Range(200.0f, 1720.0f), 0), Quaternion.identity);
            GameObject item2 = (GameObject)Instantiate(Item7, new Vector3(Random.Range(100.0f, 980.0f), Random.Range(200.0f, 1720.0f), 0), Quaternion.identity);
        }
        else if (ItemNum == 4)
        {
            GameObject item = (GameObject)Instantiate(Item8, new Vector3(Random.Range(100.0f, 980.0f), Random.Range(200.0f, 1720.0f), 0), Quaternion.identity);
        }
        else if (ItemNum == 5)
        {
            GameObject item = (GameObject)Instantiate(Item9, new Vector3(Random.Range(100.0f, 980.0f), Random.Range(200.0f, 1720.0f), 0), Quaternion.identity);
        }
        else if (ItemNum == 6)
        {
            GameObject item = (GameObject)Instantiate(Item10, new Vector3(Random.Range(100.0f, 980.0f), Random.Range(200.0f, 1720.0f), 0), Quaternion.identity);
        }
    }
}
