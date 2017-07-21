using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchControlsKit;

public class PlayerScript : MonoBehaviour {
    public float power;
    Rigidbody2D body;
    public float speed;
    public TCKDPad Pad;

	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody2D>();
        power = 1.1f;
        speed = 50.0f;
	}
	
	// Update is called once per frame
	void Update () {

        this.transform.Rotate(new Vector3(0, 0, 20.0f));
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            body.AddForce(new Vector3(-speed, 0, 0), ForceMode2D.Force);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            body.AddForce(new Vector3(speed, 0, 0), ForceMode2D.Force);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            body.AddForce(new Vector3(0, speed, 0), ForceMode2D.Force);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            body.AddForce(new Vector3(0, -speed, 0), ForceMode2D.Force);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.transform.tag == "Ball")
        {
            Debug.Log("collp");
            GameObject target = coll.gameObject;

            Vector3 inNormal = Vector3.Normalize(
                 transform.position - target.transform.position);
            Vector3 bounceVector =
                Vector3.Reflect(coll.relativeVelocity, inNormal);
            Debug.Log("inNormaly : " + inNormal.y + " bounceVectory : " + bounceVector.y);
            // 알아보기 쉽게 하기 위해 force값을 곱해 충돌시 속도를 20%로 줄였습니다. 
            // 충돌했던 제 속도를 그대로 내시려면 수식에서 force 곱해주는 부분을 빼면 됩니다. 
            target.GetComponent<Rigidbody2D>().AddForce(-inNormal * power, ForceMode2D.Impulse);
        }
    }

}
