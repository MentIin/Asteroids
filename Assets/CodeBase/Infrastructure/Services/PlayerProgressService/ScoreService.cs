using System;
using CodeBase.Interfaces.Infrastructure.Services;

namespace CodeBase.Infrastructure.Services.PlayerProgressService
{
    public class ScoreService : IScoreService
    {
        public int Score { get; private set; }
        public Action ScoreChanged { get; set; }

        public void AddScore(int amount)
        {
            Score += amount;
            ScoreChanged?.Invoke();
        }

        public void ResetScore()
        { 
            Score = 0;
            ScoreChanged?.Invoke();
        }
    }
}