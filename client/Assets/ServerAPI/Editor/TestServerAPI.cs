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
        if (GUILayout.Button("Ping"))
            CheckRequest(ServerAPI.Ping());
        if (GUILayout.Button("GetPlayersInput"))
            CheckRequest(ServerAPI.GetPlayersInput());
        if (GUILayout.Button("UpdatePlayerInput"))
            CheckRequest(ServerAPI.UpdatePlayerInput("{\"dpad\":[0,0]}"));
        if (GUILayout.Button("GetGameState"))
            CheckRequest(ServerAPI.GetGameState());
        if (GUILayout.Button("UpdateGameState"))
            CheckRequest(ServerAPI.UpdateGameState("{\"ball\":[0,0]}"));
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