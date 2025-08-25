using System;
using CodeBase.Interfaces.Infrastructure.Services;
using UniRx;
using Zenject;

namespace CodeBase.UI.ViewModels
{
    public class ScoreViewModel : IInitializable, IDisposable
    {
        public ReactiveProperty<string> Score = new();
        
        private readonly IScoreService _scoreService;

        public ScoreViewModel(IScoreService scoreService)
        {
            _scoreService = scoreService;
        }
        public void Initialize()
        {
            OnScoreChanged();
            _scoreService.ScoreChanged += OnScoreChanged;
        }

        public void Dispose()
        {
            _scoreService.ScoreChanged -= OnScoreChanged;
        }

        private void OnScoreChanged()
        {
            Score.Value = "score: " + _scoreService.Score.ToString();
        }
    }
}