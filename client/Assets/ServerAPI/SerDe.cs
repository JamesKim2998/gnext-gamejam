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
public struct GameState
{
    public Vector2 BallPosition;
    public List<PlayerState> PlayersState;
}
