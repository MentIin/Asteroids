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
        public readonly CustomVelocity velocity;

        private readonly IInputService _inputService;
        private readonly Stats _playerStats;

        private readonly IMover _mover;
        
        private int _currentHealth;


        public PlayerModel(IInputService inputService, Stats playerStats)
        {
            _inputService = inputService;
            _playerStats = playerStats;

            transformData = new TransformData(Vector2.zero);
            velocity = new CustomVelocity(transformData);
            _mover = new PhysicMover(velocity);
        }

        public void Initialize()
        {
            //_currentHealth = _playerStats.GetStat<HealthStat>().Value;
        }

        public void Tick()
        {
            _mover.Tick(_inputService.GetMoveAxis() * _playerStats.GetStat<SpeedStat>().Value, Time.deltaTime);
            velocity.Tick(Time.deltaTime);
        }
        
    }
}