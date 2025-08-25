using System.Collections.Generic;
using CodeBase.Data.StaticData;
using CodeBase.Gameplay.Factories;
using CodeBase.Gameplay.Services.SpawnService.Spawners;
using CodeBase.Interfaces.Infrastructure.Services;

namespace CodeBase.Gameplay.Services.SpawnService
{
    public class EnemySpawnService
    {
        private const int NUMBER_OF_SPAWNERS = 2;
        private readonly SpawnerFactory _spawnerFactory;
        private readonly IStaticDataService _staticDataService;
        private readonly List<EnemySpawner> _enemySpawners = new List<EnemySpawner>();

        public EnemySpawnService(SpawnerFactory spawnerFactory, IStaticDataService staticDataService)
        {
            _spawnerFactory = spawnerFactory;
            _staticDataService = staticDataService;
        }

        public void StartSpawn()
        {
            CreateEnemySpawner(EnemyType.BigAsteroid);
            CreateEnemySpawner(EnemyType.Ufo);
        }

        private void CreateEnemySpawner(EnemyType type)
        {
            EnemySpawner enemySpawner = _spawnerFactory.CreateDefaultEnemySpawner();
            int max = _staticDataService.ForMap().MaxEnemiesCount / NUMBER_OF_SPAWNERS;
            enemySpawner.SetSpawnData(type, _staticDataService.ForEnemy(type).SpawnRate, max);
            
            enemySpawner.StartSpawning();
            
            _enemySpawners.Add(enemySpawner);
        }
    }
}