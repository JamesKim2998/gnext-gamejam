using System;
using UnityEngine;
using WebSocketSharp;
using WebSocketSharp.Server;

public static class WSServer
{
    public class Laputa : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            Debug.Log(e);
            var msg = e.Data == "BALUS"
                      ? "I've been balused already..."
                      : "I'm not available now.";
            Send(msg);
        }
    }

    private static WebSocketServer _sv;

    public static void Start()
    {
        if (_sv != null) Stop();
        _sv = new WebSocketServer("ws://0.0.0.0:8080");
        _sv.AddWebSocketService<Laputa>("/Laputa");
        _sv.Start();
    }

    public static void Stop()
    {
        if (_sv == null) return;
        _sv.Stop();
        _sv = null;
    }
}

public static class WSClient
{
    private static string _domain = "ws://172.17.192.234:8080/";
    private static WebSocket _ws;

    public static void Connect()
    {
        if (_ws != null)
            _ws.Close();

        _ws = new WebSocket(_domain + "Laputa");

        _ws.OnOpen += (sender, e) =>
        {
            _ws.Send("BALUS");
            Debug.Log(e);
        };

        _ws.OnMessage += (sender, e) =>
        {
            Debug.Log(e.Data);
        };

        Debug.Log(_ws.ReadyState);
        _ws.Connect();
        Debug.Log(_ws.ReadyState);
        _ws.SendAsync("BALUS", done =>
        {
            Debug.Log("Done");
        });
    }
}
