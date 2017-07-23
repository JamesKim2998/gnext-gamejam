using System.Collections.Generic;

public static class WSServerState
{
    public static readonly List<int> JoinedPlayers = new List<int>();
    public static readonly List<PlayerInput> PlayerInputs = new List<PlayerInput>();
    public static GameState GameState = new GameState();

    public static void Reset()
    {
        lock (JoinedPlayers)
            JoinedPlayers.Clear();
        PlayerInputs.Clear();
        lock (GameState)
            GameState = new GameState();
    }

    public static void AddJoinPlayer(int deviceId)
    {
        lock (JoinedPlayers)
        {
            if (JoinedPlayers.Contains(deviceId)) return;
            JoinedPlayers.Add(deviceId);
        }
    }

    public static void RemovePlayer(int deviceId)
    {
        lock (JoinedPlayers)
        {
            if (JoinedPlayers.Contains(deviceId))
                JoinedPlayers.Remove(deviceId);
        }

        for (var i = 0; i != PlayerInputs.Count; ++i)
        {
            if (PlayerInputs[i].DeviceId == deviceId)
            {
                PlayerInputs.RemoveAt(i);
                break;
            }
        }

        lock (GameState)
        {
            for (var i = 0; i != GameState.PlayersState.Count; ++i)
            {
                if (GameState.PlayersState[i].DeviceId == deviceId)
                {
                    GameState.PlayersState.RemoveAt(i);
                    break;
                }
            }
        }
    }

    public static void SetPlayerInput(PlayerInput input)
    {
        for (var i = 0; i != PlayerInputs.Count; ++i)
        {
            if (PlayerInputs[i].DeviceId == input.DeviceId)
            {
                PlayerInputs[i] = input;
                return;
            }
        }

        WSServerState.PlayerInputs.Add(input);
    }
}
