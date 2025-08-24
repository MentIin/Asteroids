using CodeBase.Data.StatsSystem.Main;
using CodeBase.Gameplay.Factories;
using CodeBase.Gameplay.Player;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Enemies.Ufo
{
    public class Ufo : Enemy, IDamageable
    {
        private UfoModel _model;

        [Inject]
        public void Construct(Stats stats, PlayerProvider playerProvider, EnemyFactory factory)
        {
            _model = new UfoModel(stats, playerProvider, transform);
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
           ReturnToPool();
        }
    }
}