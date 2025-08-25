using CodeBase.Data.StaticData;
using CodeBase.Gameplay.Factories;
using CodeBase.Gameplay.Player;
using CodeBase.Interfaces.Infrastructure.Services;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Enemies.Ufo
{
    public class Ufo : Enemy, IDamageable
    {
        private UfoModel _model;
        private IScoreService _scoreService;
        private EnemyConfig _config;

        [Inject]
        public void Construct(EnemyConfig config, PlayerProvider playerProvider, EnemyFactory factory,
            IScoreService scoreService)
        {
            _config = config;
            _scoreService = scoreService;
            _model = new UfoModel(config.Stats, playerProvider, transform);
            TransformData = _model.transformData;
        }

        private void Update()
        {
            _model.Tick();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Vector2 force = (Vector2)transform.position - other.ClosestPoint(transform.position);
            force.Normalize();
            _model.velocity.Set(force * GameConstants.CollisionKnockbackForce);
        }

        public void TakeDamage()
        {
            _scoreService.AddScore(_config.ScoreReward);
           ReturnToPool();
        }
    }
}