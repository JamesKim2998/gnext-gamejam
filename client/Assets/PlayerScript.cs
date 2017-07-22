using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchControlsKit;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {
    public float power;
    public GameObject HpBar;
    public Canvas UICanvas;
    GameObject PlayerHp;
    Rigidbody2D body;
    public float speed;
    public float SpinSpeed;
    public TCKDPad Pad;

	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody2D>();
        power = 4.0f;
        speed = 6.0f;
        SpinSpeed = -10.0f;
        PlayerHp = Instantiate(HpBar, new Vector3(540, 350, 0), Quaternion.identity, UICanvas.transform);
        PlayerHp.transform.SetParent(UICanvas.transform, false);
    }
	
	// Update is called once per frame
	void Update () {
        PlayerHp.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 80.0f, 0);
        this.transform.Rotate(new Vector3(0, 0, SpinSpeed));
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            body.AddForce(new Vector3(-speed*1000, 0, 0), ForceMode2D.Force);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            body.AddForce(new Vector3(speed * 1000, 0, 0), ForceMode2D.Force);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            body.AddForce(new Vector3(0, speed * 1000, 0), ForceMode2D.Force);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            body.AddForce(new Vector3(0, -speed * 1000, 0), ForceMode2D.Force);
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
            
            target.GetComponent<Rigidbody2D>().AddForce(-inNormal * power, ForceMode2D.Impulse);
        }

        if(coll.transform.tag == "enemy")
        {
            Debug.Log("ou");
            PlayerHp.GetComponent<Slider>().value -= 20;
        }
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "")
    }*/

}
