using CodeBase.Data.StaticData;
using CodeBase.Interfaces.Infrastructure.Services;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Enemies.Asteroids.Small
{
    public class SmallAsteroid : Enemy, IDamageable, IPushable
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
            if (other.TryGetComponent(out IPushable pushable))
            {
                pushable.Push((other.transform.position - transform.position).normalized * GameConstants.CollisionKnockbackForce);
            }
            if (other.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage();
            }
        }

        public void TakeDamage()
        {
            _scoreService.AddScore(_config.ScoreReward);
            ReturnToPool();
        }

        public void Push(Vector2 forceVector)
        {
            _model.velocity.Set(forceVector);
        }
    }
}