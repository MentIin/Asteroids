using CodeBase.Interfaces.Infrastructure.Services;

namespace CodeBase.Infrastructure.Services.PlayerProgressService
{
    public class ScoreService : IScoreService
    {
        public Data.PlayerProgress Progress { get; set; }
    }
}