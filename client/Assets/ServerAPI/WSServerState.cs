using System.Collections.Generic;

public static class WSServerState
{
    public static readonly List<int> JoinedPlayers = new List<int>();
    public static readonly List<PlayerInput> PlayerInputs = new List<PlayerInput>();
    public static GameState GameState = new GameState();

    public static void Reset()
    {
        JoinedPlayers.Clear();
        PlayerInputs.Clear();
        GameState = new GameState();
    }

    public static void AddJoinPlayer(int deviceId)
    {
        if (JoinedPlayers.Contains(deviceId)) return;
        JoinedPlayers.Add(deviceId);
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
