using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientPlayerInputHandler : MonoBehaviour
{
    private void Update()
    {
        var myInput = new PlayerInput()
        {
            DeviceId = WSConfig.DeviceId,
            UpArrow = Input.GetKey(KeyCode.UpArrow),
            DownArrow = Input.GetKey(KeyCode.DownArrow),
            RightArrow = Input.GetKey(KeyCode.RightArrow),
            LeftArrow = Input.GetKey(KeyCode.LeftArrow),
        };

        if (WSConfig.DebugStandalone)
        {
            WSServerState.SetPlayerInput(myInput);
        }
        else
        {
            WSClient.UpdatePlayerInput(myInput);
        }
    }
}
