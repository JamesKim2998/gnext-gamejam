using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

    public float power;
    Rigidbody2D body;

    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        power = 4.0f;
    }
    
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log("collp");
        GameObject target = coll.gameObject;

        Vector3 inNormal = Vector3.Normalize(
             target.transform.position - transform.position);
        Vector3 bounceVector =
            Vector3.Reflect(coll.relativeVelocity, inNormal);

        Debug.Log("inNormaly : " + inNormal.y + " bounceVectory : " + bounceVector.y);

        // 알아보기 쉽게 하기 위해 force값을 곱해 충돌시 속도를 20%로 줄였습니다. 
        // 충돌했던 제 속도를 그대로 내시려면 수식에서 force 곱해주는 부분을 빼면 됩니다. 
        target.GetComponent<Rigidbody2D>().AddForce(inNormal + (-inNormal * power), ForceMode2D.Impulse);
    }
}
