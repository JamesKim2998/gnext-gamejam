using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ServerBallSimultor : MonoBehaviour
{
    Rigidbody2D ballRigid;
    public event Action OnP1Score;
    public event Action OnP2Score;

    void Start()
    {
        ballRigid = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.transform.tag == "item1")
        {
            Destroy(coll.gameObject);
            var forceX = Random.Range(-1f, 1f);
            var forceY = Random.Range(-1f, 1f);
            var force = new Vector2(forceX, forceY).normalized;
            ballRigid.AddForce(force, ForceMode2D.Impulse);
        }

        if (coll.transform.tag == "item2")
        {
            Destroy(coll.gameObject);
            ballRigid.velocity = new Vector3(0, 0, 0);
            Debug.Log(ballRigid.velocity);
        }

        if (coll.transform.tag == "p1net")
            if (OnP1Score != null) OnP2Score();

        if (coll.transform.tag == "p2net")
            if (OnP2Score != null) OnP1Score();
    }
}
