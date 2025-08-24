using CodeBase.Interfaces.Infrastructure.Services;

namespace CodeBase.UI.ViewModels
{
    public class ScoreViewModel
    {
        private readonly IScoreService _scoreService;

        public ScoreViewModel(IScoreService scoreService)
        {
            _scoreService = scoreService;
        }
    }
}