using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {

    [Header("Player")]
    public GameObject Player;


    [Header("Player")]
    public GameObject Enemy;


    [Header("Ball")]
    public GameObject Ball;

    [Header("Item")]
    public GameObject Item1;
    public GameObject Item2;

    int random;
    float timer;



    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        
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
            GameObject item = (GameObject)Instantiate(Item1, new Vector3(Random.Range(-2.3f, 2.3f), Random.Range(-3.5f, 3.5f), 0), Quaternion.identity);
        }
        else if(ItemNum == 1)
        {
            GameObject item = (GameObject)Instantiate(Item2, new Vector3(Random.Range(-2.3f, 2.3f), Random.Range(-3.5f, 3.5f), 0), Quaternion.identity);
        }
    }
}
