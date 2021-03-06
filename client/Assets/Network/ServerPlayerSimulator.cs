using System;
using UnityEngine;

public class ServerPlayerSimulator : MonoBehaviour
{
    public int DeviceId;
    private PlayerScript Player;
    private Rigidbody2D body;
    public float speed = 6.0f;

    public Action<int> OnEatSmallNetItem;
    public Action<int> OnEatBigNetItem;

    private void Awake()
    {
        Player = GetComponent<PlayerScript>();
        body = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        var playerInput = default(PlayerInput);
        foreach (var somePlayerInput in WSServerState.PlayerInputs)
        {
            if (somePlayerInput.DeviceId != DeviceId) continue;
            playerInput = somePlayerInput;
            break;
        }
        //Debug.Log(playerInput.DPad);
        var touchPower = 1500 * speed;
        body.AddForce(playerInput.DPad * touchPower, ForceMode2D.Force);

        var arrowPower = speed * 2000;
        if (playerInput.LeftArrow)
        {
            body.AddForce(arrowPower * Vector3.left, ForceMode2D.Force);
        }
        if (playerInput.RightArrow)
        {
            body.AddForce(arrowPower * Vector3.right, ForceMode2D.Force);
        }
        if (playerInput.UpArrow)
        {
            body.AddForce(arrowPower * Vector3.up, ForceMode2D.Force);
        }
        if (playerInput.DownArrow)
        {
            body.AddForce(arrowPower * Vector3.down, ForceMode2D.Force);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.transform.tag == "Ball")
        {
            // Debug.Log("collp");
            GameObject target = coll.gameObject;

            Vector3 inNormal = Vector3.Normalize(
                 transform.position - target.transform.position);
            Vector3 bounceVector =
                Vector3.Reflect(coll.relativeVelocity, inNormal);
            // Debug.Log("inNormaly : " + inNormal.y + " bounceVectory : " + bounceVector.y);

            target.GetComponent<Rigidbody2D>().AddForce(-inNormal * Player.power, ForceMode2D.Impulse);
        }

        if (coll.transform.tag == "Player")
        {
            Player.GroggyTimer();
            WSServer.PlayerGroggy(DeviceId);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "item3")
        {
            Destroy(collision.gameObject);
            Player.PowerTimer();
            WSServer.PlayerPowerUp(DeviceId);
        }
        if (collision.transform.tag == "item4")
        {
            Destroy(collision.gameObject);
            Player.SpeedTimer();
        }
        if (collision.transform.tag == "item5")
        {
            body.drag = 6.5f;
        }
        if (collision.transform.tag == "item6")
        {
            GameObject white = GameObject.FindWithTag("item7");
            this.transform.position = white.transform.position;
            Destroy(collision.gameObject);
            Destroy(white.gameObject);
        }
        if (collision.transform.tag == "item8")
        {
            body.drag = 0.0f;
        }

        if (collision.transform.tag == "item9")
        {
            if (OnEatSmallNetItem != null)
                OnEatSmallNetItem(DeviceId);
            Destroy(collision.gameObject);
        }

        if (collision.transform.tag == "item10")
        {
            if (OnEatBigNetItem != null)
                OnEatBigNetItem(DeviceId);
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "item5")
        {
            Destroy(collision.gameObject);
            body.drag = 0.6f;
        }
        if (collision.transform.tag == "item8")
        {
            Destroy(collision.gameObject);
            body.drag = 0.6f;
        }
    }

}