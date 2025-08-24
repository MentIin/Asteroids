using CodeBase.Data.StatsSystem.Main;
using CodeBase.Gameplay.Enviroment;
using CodeBase.Gameplay.Factories;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Enemies.Asteroids.Big
{
    public class BigAsteroid : Enemy, IArenaMember, IDamageable
    {
        private AsteroidModel _model;

        [Inject]
        public void Construct(Stats stats, EnemyFactory factory)
        {
            _model = new AsteroidModel(stats, transform);
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