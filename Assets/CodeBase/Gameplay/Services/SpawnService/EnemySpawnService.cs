using CodeBase.Data.StaticData;
using CodeBase.Gameplay.Factories;
using CodeBase.Gameplay.Services.SpawnService.Spawners;
using CodeBase.Interfaces.Infrastructure.Services;

namespace CodeBase.Gameplay.Services.SpawnService
{
    public class EnemySpawnService
    {
        private const int NUMBER_OF_SPAWNERS = 2;
        private const int NUMBER_OF_SMALL_ASTEROIDS = 3;
        private readonly SpawnerFactory _spawnerFactory;
        private readonly IStaticDataService _staticDataService;

        public EnemySpawnService(SpawnerFactory spawnerFactory, IStaticDataService staticDataService)
        {
            _spawnerFactory = spawnerFactory;
            _staticDataService = staticDataService;
        }

        public void StartSpawn()
        {
            int max = _staticDataService.ForMap().MaxEnemiesCount / NUMBER_OF_SPAWNERS;
            CreateEnemySpawner(EnemyType.BigAsteroid, max);
            CreateEnemySpawner(EnemyType.Ufo, max);
            CreateSmallAsteroidsSpawner(max);
        }

        private void CreateSmallAsteroidsSpawner(int max)
        {
            EnemySpawnerOnEnemyDeath smallAsteroidsSpawner = _spawnerFactory.CreateEnemySpawnerOnEnemyDeath();
            smallAsteroidsSpawner.Initialize();
            smallAsteroidsSpawner.SetSpawnData(EnemyType.BigAsteroid, EnemyType.SmallAsteroid,
                NUMBER_OF_SMALL_ASTEROIDS, max*NUMBER_OF_SMALL_ASTEROIDS);
        }

        private void CreateEnemySpawner(EnemyType type, int max)
        {
            EnemySpawner enemySpawner = _spawnerFactory.CreateDefaultEnemySpawner();
            enemySpawner.SetSpawnData(type, _staticDataService.ForEnemy(type).SpawnRate, max);
            enemySpawner.StartSpawning();
        }
    }
}