using CodeBase.Data;
using CodeBase.Data.StatsSystem.Main;
using CodeBase.Gameplay.Enviroment;
using CodeBase.Gameplay.Player;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Enemies.Asteroids.Small
{
    public class SmallAsteroid : Enemy, IArenaMember
    {
        private SmallAsteroidModel _model;
        private PlayerProvider _playerProvider;
        
        public TransformData TransformData => _model.transformData;

        [Inject]
        public void Construct(Stats stats, PlayerProvider playerProvider)
        {
            _playerProvider = playerProvider;
            _model = new SmallAsteroidModel(stats);
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