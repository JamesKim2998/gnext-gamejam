using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour {

    Rigidbody2D ballRigid;
	// Use this for initialization
	void Start () {
        ballRigid = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        Vector3 random = Vector3.Normalize(new Vector3(Random.Range(-1.0f, 1f), Random.Range(-1.0f, 1f), 0));
        if (coll.transform.tag == "item1")
        {
            coll.gameObject.SetActive(false);
            ballRigid.AddForce(random, ForceMode2D.Impulse);
        }

        if (coll.transform.tag == "item2")
        {
            coll.gameObject.SetActive(false);
            ballRigid.velocity = new Vector3(0, 0, 0);
            Debug.Log(ballRigid.velocity);
        }
        if(coll.transform.tag == "p1net")
        {
            ScoreManager.P2ScorePlus();
        }
        if (coll.transform.tag == "p2net")
        {
            ScoreManager.P1ScorePlus();
        }
    }
}
