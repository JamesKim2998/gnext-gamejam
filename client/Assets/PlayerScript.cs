using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchControlsKit;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {
    public static float power;
    public GameObject HpBar;
    public Canvas UICanvas;
    GameObject PlayerHp;
    Rigidbody2D body;
    public static float speed;
    public static float SpinSpeed;
    public TCKDPad Pad;

    GameObject nets;
    GameObject nets2;
    float PowerTime;
    float SpeedTime;

	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody2D>();
        power = 4.0f;
        speed = 6.0f;
        SpinSpeed = -10.0f;
        PlayerHp = Instantiate(HpBar, new Vector3(540, 350, 0), Quaternion.identity, UICanvas.transform);
        PlayerHp.transform.SetParent(UICanvas.transform, false);
        nets = GameObject.Find("net");
        nets2 = GameObject.Find("net2");
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "item3")
        {
            collision.gameObject.SetActive(false);
            StartCoroutine("PowerTimer");
        }
        if (collision.transform.tag == "item4")
        {
            collision.gameObject.SetActive(false);
            StartCoroutine("SpeedTimer");
        }
        if (collision.transform.tag == "item5")
        {

            body.drag = 6.5f;
        }
        if(collision.transform.tag == "item6")
        {
            GameObject white = GameObject.FindWithTag("item7");
            this.transform.position = white.transform.position;
            collision.gameObject.SetActive(false);
            white.gameObject.SetActive(false);
        }
        if(collision.transform.tag == "item8")
        {
            body.drag = 0.0f;
        }
        if (collision.transform.tag == "item9")
        {
            StartCoroutine("SmallNet");
            collision.gameObject.SetActive(false);
        }

        if (collision.transform.tag == "item10")
        {
            StartCoroutine("BigNet");
            collision.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "item5")
        {
            collision.gameObject.SetActive(false);
            body.drag = 0.6f;
        }
        if (collision.transform.tag == "item8")
        {
            collision.gameObject.SetActive(false);
            body.drag = 0.6f;
        }
    }

    IEnumerator PowerTimer()
    {
        Debug.Log("powerco");
        power = 9.0f;
        SpinSpeed = -30.0f;
        yield return new WaitForSeconds(5.0f);
        power = 4.0f;
        SpinSpeed = -10.0f;
    }

    IEnumerator SpeedTimer()
    {
        Debug.Log("speedco");
        speed = 10.0f;
        yield return new WaitForSeconds(5.0f);
        speed = 6.0f;
    }

    IEnumerator SmallNet()
    {
        if (GameManagerScript.firstturn)
        {
            nets = GameObject.Find("net");
            nets.transform.localScale = new Vector3(1000.0f, 1000.0f, 1);
            yield return new WaitForSeconds(5.0f);
            nets.transform.localScale = new Vector3(2000.0f, 1000.0f, 1);
        }
        else
        {
            nets2 = GameObject.Find("net2");
            nets2.transform.localScale = new Vector3(1000.0f, 1000.0f, 1);
            yield return new WaitForSeconds(5.0f);
            nets2.transform.localScale = new Vector3(2000.0f, 1000.0f, 1);
        }
    }

    IEnumerator BigNet()
    {
        if (GameManagerScript.firstturn)
        {
            nets2 = GameObject.Find("net2");
            nets2.transform.localScale = new Vector3(4000.0f, 1000.0f, 1);
            yield return new WaitForSeconds(5.0f);
            nets2.transform.localScale = new Vector3(2000.0f, 1000.0f, 1);
        }
        else
        {
            nets = GameObject.Find("net");
            nets.transform.localScale = new Vector3(4000.0f, 1000.0f, 1);
            yield return new WaitForSeconds(5.0f);
            nets.transform.localScale = new Vector3(2000.0f, 1000.0f, 1);
        }
    }
}
