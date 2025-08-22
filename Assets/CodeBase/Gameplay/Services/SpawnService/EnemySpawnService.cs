using System.Collections.Generic;
using CodeBase.Data;
using CodeBase.Data.Enums;
using CodeBase.Gameplay.Factories;
using CodeBase.Gameplay.Services.SpawnService.Spawners;
using CodeBase.Interfaces.Infrastructure.Services;

namespace CodeBase.Gameplay.Services.SpawnService
{
    public class EnemySpawnService
    {
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
            CreateEnemySpawner(EnemyType.SmallAsteroid);
            CreateEnemySpawner(EnemyType.BigAsteroid);
            CreateEnemySpawner(EnemyType.Ufo);
        }

        private void CreateEnemySpawner(EnemyType type)
        {
            EnemySpawner enemySpawner = _spawnerFactory.CreateDefaultEnemySpawner();
            enemySpawner.SetType(type);
            enemySpawner.SetSpawnRate(_staticDataService.ForEnemy(type).SpawnRate);
            enemySpawner.StartSpawning();
            _enemySpawners.Add(enemySpawner);
        }
    }
}