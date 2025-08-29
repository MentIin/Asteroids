using System;
using CodeBase.Common.Attributes;
using CodeBase.Interfaces.Infrastructure.Services;
using CodeBase.Interfaces.UI;
using UniRx;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.ViewModels
{
    public class ScoreViewModel : IInitializable, IDisposable, IViewModel
    {
        [Data("Score")]
        public ReactiveProperty<string> Score = new();
        
        private readonly IScoreService _scoreService;

        public ScoreViewModel(IScoreService scoreService)
        {
            _scoreService = scoreService;
        }
        public void Initialize()
        {
            OnScoreChanged();
            Debug.Log("ScoreViewModel Initialize with score: " + _scoreService.Score);
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