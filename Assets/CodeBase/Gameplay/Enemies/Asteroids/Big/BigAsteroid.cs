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
        private BigAsteroidModel _model;
        private PlayerProvider _playerProvider;
        
        public TransformData TransformData => _model.transformData;

        [Inject]
        public void Construct(Stats stats, PlayerProvider playerProvider, EnemyFactory factory)
        {
            _playerProvider = playerProvider;
            _model = new BigAsteroidModel(stats);
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
    }
}