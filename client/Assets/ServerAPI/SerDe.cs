using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct PlayerInput
{
    // Don't set this
    public int DeviceId;
    public Vector2 DPad;
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
    public Dictionary<int, PlayerState> PlayersState;
}
