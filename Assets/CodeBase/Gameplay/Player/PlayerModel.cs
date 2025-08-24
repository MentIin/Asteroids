using CodeBase.Data;
using CodeBase.Data.StatsSystem;
using CodeBase.Data.StatsSystem.Main;
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
        
        private int _currentHealth;


        public PlayerModel(IInputService inputService, Stats playerStats)
        {
            _inputService = inputService;
            _playerStats = playerStats;

            transformData = new TransformData(Vector2.zero);
            velocity = new CustomVelocity(transformData);
        }

        public void Initialize()
        {
            _currentHealth = _playerStats.GetStat<HealthStat>().Value;
        }

        public void Tick()
        {
            velocity.AddForce(_inputService.GetMovement() * transformData.Direction * Time.deltaTime * _playerStats.GetStat<SpeedStat>().Value);
            velocity.AddAngularForce(_inputService.GetRotation());
            velocity.Tick(Time.deltaTime);
        }
    }
}