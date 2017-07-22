using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct PlayerInput
{
    public int DeviceId;
    public Vector2 DPad;
    public bool UpArrow;
    public bool DownArrow;
    public bool RightArrow;
    public bool LeftArrow;
}

[Serializable]
public struct PlayerState
{
    public int DeviceId;
    public Vector2 Position;
}

[Serializable]
public class GameState
{
    public Vector2 BallPosition;
    public List<PlayerState> PlayersState = new List<PlayerState>();
    public GameState Clone()
    {
        var ret = new GameState
        {
            BallPosition = this.BallPosition,
        };
        ret.PlayersState.AddRange(this.PlayersState);
        return ret;
    }
}
