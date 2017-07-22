using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour {

    public float power;
    Rigidbody2D body;
    public GameObject HpBar;
    public Canvas UICanvas;
    GameObject EnemyHp;

    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        power = 4.0f;

        if (UICanvas)
        {
            EnemyHp = Instantiate(HpBar, new Vector3(540, 350, 0), Quaternion.identity, UICanvas.transform);
            EnemyHp.transform.SetParent(UICanvas.transform, false);
        }
    }
    
	// Update is called once per frame
	void Update () {
        if (EnemyHp != null)
            EnemyHp.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 80.0f, 0);
        this.transform.Rotate(new Vector3(0, 0, 10.0f));
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.transform.tag == "Ball")
        {
            Debug.Log("collp");
            GameObject target = coll.gameObject;

            Vector3 inNormal = Vector3.Normalize(
                 transform.position - target.transform.position);
            //Vector3 bounceVector =  Vector3.Reflect(coll.relativeVelocity, inNormal);
            // 바운스 벡터 뭐가 문제일까
            // Debug.Log("inNormaly : " + inNormal.y + " bounceVectory : " + bounceVector.y);
            target.GetComponent<Rigidbody2D>().AddForce(-inNormal * power, ForceMode2D.Impulse);
        }

        if (coll.transform.tag == "Player")
        {
            EnemyHp.GetComponent<Slider>().value -= 20;
        }
    }
}
