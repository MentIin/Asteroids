using System;

namespace CodeBase.Interfaces.Infrastructure.Services
{
    public interface IScoreService
    {
        public int Score { get; }
        public Action ScoreChanged { get; set; }
        public void AddScore(int amount);
        public void ResetScore();
    }
}