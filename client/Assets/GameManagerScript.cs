using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {

    [Header("Player")]
    public GameObject Player;
    public float PlayerPower;

    [Header("Player")]
    public GameObject Enemy;
    public float EnemyPower;

    [Header("Ball")]
    public GameObject Ball;

    // Use this for initialization
    void Start () {
        PlayerPower = Player.GetComponent<PlayerScript>().power;
        EnemyPower = Enemy.GetComponent<EnemyScript>().power;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
