using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public static class ServerAPI
{
    private const string _domain = "http://172.17.192.234:8080/";

    private static UnityWebRequest Get(string subUrl)
    {
        var r = UnityWebRequest.Get(_domain + subUrl);
        r.Send();
        return r;
    }

    private static UnityWebRequest Post(string subUrl, string postData)
    {
        var r = UnityWebRequest.Post(_domain + subUrl, postData);
        r.SetRequestHeader("Content-Type", "application/json");
        r.Send();
        return r;
    }

    public static UnityWebRequest Ping()
    {
        return Get("ping");
    }

    public static UnityWebRequest GetPlayersInput()
    {
        return Get("get_players_input");
    }

    public static UnityWebRequest UpdatePlayerInput(string data)
    {
        return Post(
            "update_player_input?device_id=" + SystemInfo.deviceUniqueIdentifier,
            data);
    }

    public static UnityWebRequest GetGameState()
    {
        return Get("get_game_state");
    }

    public static UnityWebRequest UpdateGameState(string data)
    {
        return Post("update_game_state", data);
    }
}
