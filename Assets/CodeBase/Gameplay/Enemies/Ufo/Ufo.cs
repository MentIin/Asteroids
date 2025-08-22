using CodeBase.Data;
using CodeBase.Data.StatsSystem.Main;
using CodeBase.Gameplay.Enviroment;
using CodeBase.Gameplay.Factories;
using CodeBase.Gameplay.Player;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Enemies.Ufo
{
    public class Ufo : Enemy, IArenaMember
    {
        private UfoModel _model;
        private PlayerProvider _playerProvider;
        
        public TransformData TransformData => _model.transformData;

        [Inject]
        public void Construct(Stats stats, PlayerProvider playerProvider, EnemyFactory factory)
        {
            _playerProvider = playerProvider;
            _model = new UfoModel(stats);
        }

        private void Update()
        {
            _model.SetMoveDirection(
                _playerProvider.Player.TransformData.Position - (Vector2)transform.position
            );
            _model.Tick();
        }
    }
}