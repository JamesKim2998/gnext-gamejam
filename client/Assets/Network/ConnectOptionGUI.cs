using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConnectOptionGUI : MonoBehaviour
{
    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(0, 0, Screen.width / 4, Screen.height));

        var textFieldStyle = new GUIStyle(GUI.skin.textField);
        textFieldStyle.fontSize = 40;
        WSConfig.ServerAddr = GUILayout.TextField(WSConfig.ServerAddr, textFieldStyle);

        var btnStyle = new GUIStyle(GUI.skin.button);
        btnStyle.fontSize = 40;
        if (GUILayout.Button("Client", btnStyle))
        {
            Init.IsClient = true;
            Init.DebugStandalone = false;
            SceneManager.LoadScene("main");
        }

        if (GUILayout.Button("Server", btnStyle))
        {
            Init.IsClient = false;
            Init.DebugStandalone = false;
            SceneManager.LoadScene("main");
        }

        if (GUILayout.Button("DebugStandalone", btnStyle))
        {
            Init.DebugStandalone = true;
            SceneManager.LoadScene("main");
        }
        GUILayout.EndArea();
    }
}
