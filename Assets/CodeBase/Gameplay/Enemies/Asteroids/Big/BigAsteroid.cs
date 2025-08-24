using CodeBase.Data.Enums;
using CodeBase.Data.StatsSystem.Main;
using CodeBase.Gameplay.Enemies.Asteroids.Small;
using CodeBase.Gameplay.Factories;
using CodeBase.Gameplay.ObjectPool;
using CodeBase.Interfaces.Infrastructure.Services;
using UnityEngine;
using Zenject;
using IPoolable = CodeBase.Gameplay.ObjectPool.IPoolable;

namespace CodeBase.Gameplay.Enemies.Asteroids.Big
{
    public class BigAsteroid : Enemy, IDamageable
    {
        private const int NUMBER_OF_SMALL_ASTEROOIDS = 3;
        private AsteroidModel _model;
        
        private ObjectPool<SmallAsteroid> _smallAsteroidPool;
        private IRandomizerService _randomizerService;
        private int _smallAsteroidsCount;

        [Inject]
        public void Construct(Stats stats, EnemyFactory factory, IRandomizerService randomizerService)
        {
            _randomizerService = randomizerService;
            _model = new AsteroidModel(stats, transform);
            TransformData = _model.transformData;
            _smallAsteroidPool = new ObjectPool<SmallAsteroid>(
                () => factory.SpawnEnemy(EnemyType.SmallAsteroid) as SmallAsteroid,
                onRelease: smallAsteroid => smallAsteroid.gameObject.SetActive(false),
                onDestroy: smallAsteroid => GameObject.Destroy(smallAsteroid.gameObject),
                maxSize: NUMBER_OF_SMALL_ASTEROOIDS);
                
        }

        private void Update()
        {
            _model.Tick();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Vector2 force = (Vector2)transform.position - other.ClosestPoint(transform.position);
            force.Normalize();
            _model.velocity.Set(force * GameConstants.CollisionKnockbackForce);
        }

        public void TakeDamage()
        {
            SpawnSmallAsteroids();
            this.gameObject.SetActive(false);
        }

        private void SpawnSmallAsteroids()
        {
            // Spawn 3 small asteroids at the position of the big asteroid
            for (int i = 0; i < NUMBER_OF_SMALL_ASTEROOIDS; i++)
            {
                SmallAsteroid smallAsteroid = _smallAsteroidPool.Get();
                smallAsteroid.OnReturnToPool += OnReturnToPool;
                smallAsteroid.TransformData.Position = TransformData.Position;
                smallAsteroid.TransformData.Rotation = _randomizerService.RandomRotation();
                _smallAsteroidsCount++;
            }
        }

        private void OnReturnToPool(IPoolable obj)
        {
            _smallAsteroidsCount--;
            if (_smallAsteroidsCount <= 0)
            {
                ReturnToPool();
            }
        }
    }
}