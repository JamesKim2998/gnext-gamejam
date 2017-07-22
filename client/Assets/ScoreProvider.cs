using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IScoreProvider
{
    int P1Score { get; }
    int P2Score { get; }
}

public class ZeroScoreProvider : IScoreProvider
{
    public int P1Score { get { return 0; } }
    public int P2Score { get { return 0; } }
}
