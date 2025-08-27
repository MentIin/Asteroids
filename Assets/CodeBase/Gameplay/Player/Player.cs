using CodeBase.Data;
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
        private PlayerModel _playerModel;
        private IInputService _inputService;
        private ProjectileFactory _projectileFactory;
        [SerializeField] private ParticleSystem _particleSystem;
        
        public TransformData TransformData => _playerModel.transformData;
        public float Speed => _playerModel.velocity.Speed;


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
            _playerModel.Died += () => Destroy(gameObject);
        }
        private void Update()
        {
            _playerModel.Tick();
            transform.rotation = _playerModel.transformData.RotationQuaternion;
            if (_inputService.GetBaseAttack())
                ShootBullet();
            if (_inputService.GetSpecialAttack())
                SpecialAttack();

            if (!_particleSystem.isPlaying && _playerModel.IsInvulnerable)
            {
                _particleSystem.Play();
            }else if (_particleSystem.isPlaying && !_playerModel.IsInvulnerable)
            {
                _particleSystem.Stop();
            }
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
            if (_playerModel.IsInvulnerable) return;
            
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