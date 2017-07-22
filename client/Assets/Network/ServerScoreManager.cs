using UnityEngine;

public class ServerScoreManager : MonoBehaviour, IScoreProvider
{
    private int _scoreP1;
    private int _scoreP2;

    public int P1Score { get { return _scoreP1; } }
    public int P2Score { get { return _scoreP2; } }

    public void P1ScorePlus()
    {
        ++_scoreP1;
        Broadcast();
    }

    public void P2ScorePlus()
    {
        ++_scoreP2;
        Broadcast();
    }

    private void Broadcast()
    {
        WSServer.ChangeScore(new SerDeScore { ScoreP1 = _scoreP1, ScoreP2 = _scoreP2 });
    }
}