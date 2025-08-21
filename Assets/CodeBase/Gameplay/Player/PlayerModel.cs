using CodeBase.Data;
using CodeBase.Data.StatsSystem;
using CodeBase.Data.StatsSystem.Main;
using CodeBase.Gameplay.Movers;
using CodeBase.Gameplay.Physic;
using CodeBase.Interfaces.Infrastructure.Services;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Player
{
    public class PlayerModel : ITickable, IInitializable
    {
        public readonly TransformData transformData;
        
        private readonly IInputService _inputService;
        private readonly Stats _playerStats;
        
        private readonly CustomVelocity _velocity;
        private readonly IMover _mover;
        
        private int _currentHealth;


        public PlayerModel(IInputService inputService, Stats playerStats)
        {
            _inputService = inputService;
            _playerStats = playerStats;

            transformData = new TransformData(Vector2.zero);
            _velocity = new CustomVelocity(transformData);
            _mover = new PhysicMover(_velocity);
        }

        public void Initialize()
        {
            //_currentHealth = _playerStats.GetStat<HealthStat>().Value;
        }

        public void Tick()
        {
            _mover.Tick(_inputService.GetMoveAxis(), Time.deltaTime);
            _velocity.Tick(Time.deltaTime);
        }

        public void OnCollisionEnter2D(Collision2D other)
        {
            _velocity.HandleCollision(other);
        }
    }
}