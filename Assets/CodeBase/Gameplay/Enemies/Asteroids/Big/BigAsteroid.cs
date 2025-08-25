using CodeBase.Data.StaticData;
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
        private const int NUMBER_OF_SMALL_ASTEROIDS = 3;
        private AsteroidModel _model;
        
        private ObjectPool<SmallAsteroid> _smallAsteroidPool;
        private IRandomizerService _randomizerService;
        private int _smallAsteroidsCount;
        private IScoreService _scoreService;
        private EnemyConfig _config;

        [Inject]
        public void Construct(EnemyConfig config, EnemyFactory factory, IScoreService scoreService, IRandomizerService randomizerService)
        {
            _config = config;
            _scoreService = scoreService;
            _randomizerService = randomizerService;
            _model = new AsteroidModel(config.Stats);
            TransformData = _model.transformData;
            _smallAsteroidPool = new ObjectPool<SmallAsteroid>(
                () => factory.SpawnEnemy(EnemyType.SmallAsteroid) as SmallAsteroid,
                onGet: smallAsteroid => smallAsteroid.gameObject.SetActive(true),
                onRelease: smallAsteroid => smallAsteroid.gameObject.SetActive(false),
                onDestroy: smallAsteroid => GameObject.Destroy(smallAsteroid.gameObject),
                maxSize: NUMBER_OF_SMALL_ASTEROIDS);
            _smallAsteroidPool.PreWarm(NUMBER_OF_SMALL_ASTEROIDS);
                
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
            _scoreService.AddScore(_config.ScoreReward);
        }

        private void SpawnSmallAsteroids()
        {
            // Spawn 3 small asteroids at the position of the big asteroid
            for (int i = 0; i < NUMBER_OF_SMALL_ASTEROIDS; i++)
            {
                SmallAsteroid smallAsteroid = _smallAsteroidPool.Get();
                smallAsteroid.OnReturnToPool += OnSmallAsteroidReturnToPool;
                smallAsteroid.TransformData.Position = TransformData.Position;
                smallAsteroid.TransformData.Rotation = _randomizerService.RandomRotation();
                _smallAsteroidsCount++;
            }
        }

        private void OnSmallAsteroidReturnToPool(IPoolable obj)
        {
            _smallAsteroidsCount--;
            
            if (_smallAsteroidsCount <= 0)
            {
                for (int i = 0; i < NUMBER_OF_SMALL_ASTEROIDS; i++)
                {
                    obj.OnReturnToPool -= OnSmallAsteroidReturnToPool;
                }
                ReturnToPool();
            }
        }
    }
}