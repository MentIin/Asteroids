using System.Collections.Generic;
using CodeBase.Data;
using CodeBase.Gameplay.Factories;
using CodeBase.Gameplay.Services.SpawnService.Spawners;

namespace CodeBase.Gameplay.Services.SpawnService
{
    public class SpawnService
    {
        private readonly SpawnerFactory _spawnerFactory;
        private readonly List<EnemySpawner> _enemySpawners = new List<EnemySpawner>();

        public SpawnService(SpawnerFactory spawnerFactory)
        {
            _spawnerFactory = spawnerFactory;
        }

        public void StartSpawn()
        {
            EnemySpawner enemySpawner = _spawnerFactory.CreateDefaultEnemySpawner();
            enemySpawner.SetType(EnemyType.BigAsteroid);
            enemySpawner.SetSpawnRate(1f);
            enemySpawner.StartSpawning();
            _enemySpawners.Add(enemySpawner);
            
            enemySpawner = _spawnerFactory.CreateDefaultEnemySpawner();
            enemySpawner.SetType(EnemyType.Ufo);
            enemySpawner.SetSpawnRate(4f);
            enemySpawner.StartSpawning();
        }
    }
}