using UnityEngine;

public class ClientScoreManager : MonoBehaviour, IScoreProvider
{
    private int _scoreP1;
    private int _scoreP2;

    public int P1Score { get { return _scoreP1; } }
    public int P2Score { get { return _scoreP2; } }

    private void Awake()
    {
        WSClientBroadcast.ChangeScore += OnChangeScore;
    }

    private void OnDestroy()
    {
        WSClientBroadcast.ChangeScore -= OnChangeScore;
    }

    private void OnChangeScore(SerDeScore score)
    {
        _scoreP1 = score.ScoreP1;
        _scoreP2 = score.ScoreP2;
    }
}

