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
    public GameObject Item1;
    public GameObject Item2;

    int random;
    float timer;
    float GameTime;



    // Use this for initialization
    void Start () {
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
        random = Random.Range(0, 10);
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
        int ItemNum = rand % 2;

        if(ItemNum == 0)
        {
            GameObject item = (GameObject)Instantiate(Item1, new Vector3(Random.Range(100.0f, 980.0f), Random.Range(200.0f, 1720.0f), 0), Quaternion.identity);
        }
        else if(ItemNum == 1)
        {
            GameObject item = (GameObject)Instantiate(Item2, new Vector3(Random.Range(100.0f, 980.0f), Random.Range(200.0f, 1720.0f), 0), Quaternion.identity);
        }
    }
}
