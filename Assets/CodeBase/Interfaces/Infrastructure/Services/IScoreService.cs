namespace CodeBase.Interfaces.Infrastructure.Services
{
    public interface IScoreService
    {
        public int Score { get; }
        public void AddScore(int amount);
        public void ResetScore();
    }
}