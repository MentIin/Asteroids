using CodeBase.Data.StatsSystem.Main;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Enemies.Asteroids.Small
{
    public class SmallAsteroid : Enemy, IDamageable
    {
        private AsteroidModel _model;

        [Inject]
        public void Construct(Stats stats)
        {
            _model = new AsteroidModel(stats);
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