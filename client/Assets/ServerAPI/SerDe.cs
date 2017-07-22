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
    public int Queue;
    public Vector2 Position;
    public float Hp;
}

[Serializable]
public class GameState
{
    public Vector2 BallPosition;
    public Vector2 BallVelocity;
    public List<PlayerState> PlayersState = new List<PlayerState>();
    public GameState Clone()
    {
        var ret = new GameState
        {
            BallPosition = this.BallPosition,
            BallVelocity = this.BallVelocity,
        };
        ret.PlayersState.AddRange(this.PlayersState);
        return ret;
    }
}

[Serializable]
public class SerDeSpawnItem
{
    public int ItemType;
    public Vector2 Position;
    public int NetworkId;
}

[Serializable]
public class SerDeDestroyItem
{
    public int NetworkId;
}