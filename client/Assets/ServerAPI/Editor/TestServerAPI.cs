using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class TestServerAPI : EditorWindow
{
    [MenuItem("GNext/Test/TestServerAPI")]
    public static void Open()
    {
        var w = EditorWindow.GetWindow(typeof(TestServerAPI));
        w.titleContent = new GUIContent("TestServerAPI");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Server Start"))
            WSServer.Start();
        if (GUILayout.Button("Server Stop"))
            WSServer.Stop();
        GUILayout.Space(32);

        if (GUILayout.Button("Client Connect"))
            WSClient.Connect();
        if (GUILayout.Button("Client Join"))
            WSClient.Join();
        if (GUILayout.Button("Client UpdatePlayersInput"))
            WSClient.UpdatePlayerInput(
                new PlayerInput { DPad = Vector2.one });
        if (GUILayout.Button("Client GetGameState"))
            WSClient.GetGameState();
    }

    private void CheckRequest(UnityWebRequest r)
    {
        EditorApplication.delayCall += () =>
        {
            if (!r.isDone) CheckRequest(r);
            else Debug.Log("Done: " + r.downloadHandler.text);
        };
    }
}