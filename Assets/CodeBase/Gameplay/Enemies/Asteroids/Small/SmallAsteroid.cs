using CodeBase.Data.StaticData;
using CodeBase.Data.StatsSystem.Main;
using CodeBase.Interfaces.Infrastructure.Services;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Enemies.Asteroids.Small
{
    public class SmallAsteroid : Enemy, IDamageable
    {
        private AsteroidModel _model;
        private EnemyConfig _config;
        private IScoreService _scoreService;

        [Inject]
        public void Construct(EnemyConfig config, IScoreService scoreService)
        {
            _scoreService = scoreService;
            _config = config;
            _model = new AsteroidModel(config.Stats);
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