using CodeBase.Data;
using CodeBase.Data.StatsSystem.Main;
using CodeBase.Gameplay.Enviroment;
using CodeBase.Gameplay.Factories;
using CodeBase.Gameplay.Player;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Enemies.Asteroids.Big
{
    public class BigAsteroid : Enemy, IArenaMember
    {
        private AsteroidModel _model;
        private PlayerProvider _playerProvider;
        
        public TransformData TransformData => _model.transformData;

        [Inject]
        public void Construct(Stats stats, PlayerProvider playerProvider, EnemyFactory factory, TickableManager tickableManager)
        {
            _playerProvider = playerProvider;
            _model = new AsteroidModel(stats, _playerProvider, transform);
            tickableManager.Add(_model);
            _model.Initialize();
        }

        private void Start()
        {
            _model.SetMoveDirection(
                _playerProvider.Player.TransformData.Position - (Vector2)transform.position
            );
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
    }
}