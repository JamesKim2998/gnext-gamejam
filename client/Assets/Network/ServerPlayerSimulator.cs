using UnityEngine;

public class ServerPlayerSimulator : MonoBehaviour
{
    public int DeviceId;
    private Rigidbody2D body;
    public float speed = 6.0f;

    private void Awake()
    {
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
}