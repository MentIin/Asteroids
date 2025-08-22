using System;
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
        public void Construct(Stats stats, PlayerProvider playerProvider, EnemyFactory factory,
            TickableManager tickableManager)
        {
            _playerProvider = playerProvider;
            _model = new UfoModel(stats, playerProvider, transform);
            tickableManager.Add(_model);
            
            _model.Initialize();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Vector2 force = (Vector2)transform.position - other.ClosestPoint(transform.position);
            force.Normalize();
            _model.velocity.Set(force * GameConstants.CollisionKnockbackForce);
        }
    }
}