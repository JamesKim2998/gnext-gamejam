using UnityEngine;

public class ServerPlayerSimulator : MonoBehaviour
{
    public int DeviceId;
    private PlayerScript Player;
    private Rigidbody2D body;
    public float speed = 6.0f;

    private void Awake()
    {
        Player = GetComponent<PlayerScript>();
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        var playerInput = default(PlayerInput);
        foreach (var somePlayerInput in WSServerState.PlayerInputs)
        {
            if (somePlayerInput.DeviceId != DeviceId) continue;
            playerInput = somePlayerInput;
            break;
        }

        if (playerInput.LeftArrow)
        {
            body.AddForce(new Vector3(-speed * 1000, 0, 0), ForceMode2D.Force);
        }
        if (playerInput.RightArrow)
        {
            body.AddForce(new Vector3(speed * 1000, 0, 0), ForceMode2D.Force);
        }
        if (playerInput.UpArrow)
        {
            body.AddForce(new Vector3(0, speed * 1000, 0), ForceMode2D.Force);
        }
        if (playerInput.DownArrow)
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

            target.GetComponent<Rigidbody2D>().AddForce(-inNormal * Player.power, ForceMode2D.Impulse);
        }

        if (coll.transform.tag == "Player")
            Player.PlayerHPValue -= 20;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "item3")
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
        if (collision.transform.tag == "item6")
        {
            GameObject white = GameObject.FindWithTag("item7");
            this.transform.position = white.transform.position;
            collision.gameObject.SetActive(false);
            white.gameObject.SetActive(false);
        }
        if (collision.transform.tag == "item8")
        {
            body.drag = 0.0f;
        }

        // TODO
        if (collision.transform.tag == "item9")
        {
            StartCoroutine("SmallNet");
            collision.gameObject.SetActive(false);
        }

        // TODO
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

}