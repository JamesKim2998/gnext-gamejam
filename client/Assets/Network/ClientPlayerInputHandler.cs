using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientPlayerInputHandler : MonoBehaviour
{
    GameObject PlayerChecker;

    private void Start()
    {
        PlayerChecker = GameObject.Find("PlayerChecker");
    }

    private void Update()
    {
        WSClient.UpdatePlayerInput(new PlayerInput()
        {
            DeviceId = WSConfig.DeviceId,
            DPad = TouchScreen.inNormal,
            UpArrow = Input.GetKey(KeyCode.UpArrow),
            DownArrow = Input.GetKey(KeyCode.DownArrow),
            RightArrow = Input.GetKey(KeyCode.RightArrow),
            LeftArrow = Input.GetKey(KeyCode.LeftArrow),
        });

        PlayerChecker.transform.position = new Vector3(this.transform.position.x, this.transform.position.y+100, 0);
        // Debug.Log(TouchScreen.inNormal);
    }
}
