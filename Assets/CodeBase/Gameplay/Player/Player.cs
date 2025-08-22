using System;
using CodeBase.Data;
using CodeBase.Data.StatsSystem.Main;
using CodeBase.Gameplay.Enviroment;
using CodeBase.Interfaces.Infrastructure.Services;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Player
{
    public class Player : MonoBehaviour, IArenaMember
    {
        private PlayerModel _playerModel;
        public TransformData TransformData => _playerModel.transformData;


        [Inject]
        public void Construct(IInputService inputService, Stats playerStats, TickableManager tickableManager)
        {
            _playerModel = new PlayerModel(inputService, playerStats);
            tickableManager.Add(_playerModel);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Vector2 force = (Vector2)transform.position - other.ClosestPoint(transform.position);
            force.Normalize();
            _playerModel.velocity.Set(force * GameConstants.CollisionKnockbackForce);
        }

    }
}