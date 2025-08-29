using System;
using CodeBase.Data;
using CodeBase.Data.Signals;
using CodeBase.Data.StaticData;
using CodeBase.Data.StatsSystem.Main;
using CodeBase.Gameplay.Enviroment;
using CodeBase.Gameplay.Factories;
using CodeBase.Interfaces.Infrastructure.Services;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Player
{
    public class Player : MonoBehaviour, IArenaMember, IDamageable, IPushable
    {
        [SerializeField] private Collider2D _collider;
        [SerializeField] private ParticleSystem _particleSystem;
        
        private PlayerModel _playerModel;
        private SignalBus _signalBus;

        public TransformData TransformData => _playerModel.transformData;
        public float Speed => _playerModel.velocity.Speed;
        public int LaserCharges => _playerModel.LaserCharges;
        public float LaserChargeReload => _playerModel.LaserChargesReload;


        [Inject]
        public void Construct(IInputService inputService, Stats playerStats, 
            ProjectileFactory projectileFactory, SignalBus signalBus)
        {
            _signalBus = signalBus;
            _playerModel = new PlayerModel(inputService, projectileFactory, playerStats);
        }
        private void Start()
        {
            _playerModel.Initialize();
            _playerModel.Died += PlayerModelOnDied;
        }

        private void PlayerModelOnDied()
        {
            Destroy(this.gameObject);
            _signalBus.Fire(new PlayerDiedSignal());
        }

        private void Update()
        {
            _playerModel.Tick();
            transform.rotation = _playerModel.transformData.RotationQuaternion;

            if (!_particleSystem.isPlaying && _playerModel.IsInvulnerable)
            {
                _particleSystem.Play();
            }else if (_particleSystem.isPlaying && !_playerModel.IsInvulnerable)
            {
                _particleSystem.Stop();
            }

            _collider.enabled = !_playerModel.IsInvulnerable;
        }

        

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IPushable pushable))
            {
                pushable.Push((other.transform.position - transform.position).normalized * GameConstants.CollisionKnockbackForce);
            }
        }

        public void TakeDamage()
        {
            _playerModel.TakeDamage();
        }

        public void Push(Vector2 forceVector)
        {
            if (_playerModel.IsInvulnerable) return;
            _playerModel.velocity.Set(forceVector);
        }
    }
}