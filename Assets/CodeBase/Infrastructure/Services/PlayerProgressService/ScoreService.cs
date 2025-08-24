using CodeBase.Interfaces.Infrastructure.Services;

namespace CodeBase.Infrastructure.Services.PlayerProgressService
{
    public class ScoreService : IScoreService
    {
        public int Score { get; private set; }
        public void AddScore(int amount)
        {
            Score += amount;
        }

        public void ResetScore()
        { 
            Score = 0;
        }
    }
}