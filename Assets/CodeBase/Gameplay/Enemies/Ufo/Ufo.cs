using CodeBase.Data.StaticData;
using CodeBase.Gameplay.Factories;
using CodeBase.Gameplay.Services.Providers;
using CodeBase.Interfaces.Infrastructure.Services;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Enemies.Ufo
{
    public class Ufo : Enemy, IDamageable, IPushable
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