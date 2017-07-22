using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientPlayerInputHandler : MonoBehaviour
{
    private void Update()
    {
        WSClient.UpdatePlayerInput(new PlayerInput()
        {
            DeviceId = WSConfig.DeviceId,
            UpArrow = Input.GetKey(KeyCode.UpArrow),
            DownArrow = Input.GetKey(KeyCode.DownArrow),
            RightArrow = Input.GetKey(KeyCode.RightArrow),
            LeftArrow = Input.GetKey(KeyCode.LeftArrow),
        });
    }
}
