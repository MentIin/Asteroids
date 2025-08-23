using System;
using CodeBase.Data;
using CodeBase.Data.Enums;
using CodeBase.Data.StatsSystem.Main;
using CodeBase.Gameplay.Enviroment;
using CodeBase.Gameplay.Factories;
using CodeBase.Gameplay.Services.InputService;
using CodeBase.Interfaces.Infrastructure.Services;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Player
{
    public class Player : MonoBehaviour, IArenaMember
    {
        private PlayerModel _playerModel;
        private IInputService _inputService;
        private ProjectileFactory _projectileFactory;
        public TransformData TransformData => _playerModel.transformData;


        [Inject]
        public void Construct(IInputService inputService, Stats playerStats, ProjectileFactory projectileFactory)
        {
            _playerModel = new PlayerModel(inputService, playerStats);
            _inputService = inputService;
            _projectileFactory = projectileFactory;
        }
        private void Start()
        {
            _playerModel.Initialize();
        }
        private void Update()
        {
            _playerModel.Tick();
            transform.rotation = _playerModel.transformData.RotationQuaternion;
            if (_inputService.GetBaseAttack())
                ShootBullet();
            if (_inputService.GetSpecialAttack())
                SpecialAttack();
        }

        private void SpecialAttack()
        {
            _projectileFactory.CreateProjectile(ProjectileType.Laser, transform.position, transform.rotation);
        }

        private void ShootBullet()
        {
            _projectileFactory.CreateProjectile(ProjectileType.Bullet, transform.position, transform.rotation);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Vector2 force = (Vector2)transform.position - other.ClosestPoint(transform.position);
            force.Normalize();
            _playerModel.velocity.Set(force * GameConstants.CollisionKnockbackForce);
        }

    }
}