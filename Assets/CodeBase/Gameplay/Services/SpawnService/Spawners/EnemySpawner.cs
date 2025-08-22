using System;
using System.Threading;
using CodeBase.Data;
using CodeBase.Data.Enums;
using CodeBase.Gameplay.Enviroment;
using CodeBase.Gameplay.Factories;
using CodeBase.Interfaces.Infrastructure.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Gameplay.Services.SpawnService.Spawners
{
    public class EnemySpawner
    {
        private readonly Arena _arena;
        private readonly EnemyFactory _factory;
        private readonly IRandomizerService _randomizerService;
        private EnemyType _enemyType;
        private float _spawnRate = 1f;
        
        private CancellationTokenSource _spawnCts;
        private bool _isSpawning;

        public EnemySpawner(EnemyFactory factory, IRandomizerService randomizerService,
            Arena arena)
        {
            _arena = arena;
            _factory = factory;
            _randomizerService = randomizerService;
        }

        public void StartSpawning()
        {
            if (_isSpawning) return;
            
            _isSpawning = true;
            _spawnCts = new CancellationTokenSource();
            SpawnLoop(_spawnCts.Token).Forget();
        }

        public void StopSpawning()
        {
            if (!_isSpawning) return;
            
            _spawnCts?.Cancel();
            _spawnCts?.Dispose();
            _spawnCts = null;
            _isSpawning = false;
        }

        public void SetSpawnRate(float newRate)
        {
            if (newRate <= 0)
                throw new ArgumentException("Spawn rate must be positive");
            
            _spawnRate = newRate;
        }

        public void SetType(EnemyType enemyType)
        {
            _enemyType = enemyType;
        }

        private async UniTaskVoid SpawnLoop(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                Spawn();
                await UniTask
                    .WaitForSeconds(1/_spawnRate, cancellationToken: cancellationToken)
                    .SuppressCancellationThrow();
            }
        }

        private void Spawn()
        {
            Vector2 pos = _randomizerService.GetRandomPositionOnBoundsEdge(_arena.Size, _arena.Center, 10f);

            _factory.SpawnEnemy(_enemyType, pos);
        }
    }
}