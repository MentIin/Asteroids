using System.Threading;
using CodeBase.Data.Enums;
using CodeBase.Gameplay.Enemies;
using CodeBase.Gameplay.Enviroment;
using CodeBase.Gameplay.Factories;
using CodeBase.Gameplay.ObjectPool;
using CodeBase.Interfaces.Infrastructure.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Gameplay.Services.SpawnService.Spawners
{
    public class EnemySpawner
    {
        private const float ADDITIONAL_OFFSET = 20f;
        
        private readonly Arena _arena;
        private readonly EnemyFactory _factory;
        private readonly IRandomizerService _randomizerService;
        private EnemyType _enemyType;
        private float _spawnRate = 1f;
        
        private CancellationTokenSource _spawnCts;
        private bool _isSpawning;
        
        private ObjectPool<Enemy> _enemyPool;

        private int _maxEnemies;

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

        public void SetSpawnData(EnemyType type, float newRate, int max)
        {
            _enemyType = type;
            _maxEnemies = max;
            _spawnRate = newRate;
            
            _enemyPool = new ObjectPool<Enemy>(
                () => _factory.SpawnEnemy(_enemyType),
                onGet: enemy =>  enemy.gameObject.SetActive(true),
                onRelease: enemy => enemy.gameObject.SetActive(false),
                onDestroy: enemy => GameObject.Destroy(enemy.gameObject),
                maxSize: _maxEnemies);
            _enemyPool.PreWarm(_maxEnemies);
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
            if (_enemyPool.CountInactive == 0)
                return;
            Vector2 pos = _randomizerService.GetRandomPositionOnBoundsEdge(_arena.Size, _arena.Center, ADDITIONAL_OFFSET);

            Enemy enemy = _enemyPool.Get();
            enemy.TransformData.Position = pos;
            enemy.TransformData.Rotation = GetRandomRotation(pos);
        }
        private float GetRandomRotation(Vector2 position)
        {
            Vector2 randomPointInBounds = new Vector2(
                _randomizerService.Range(-_arena.Size.x, _arena.Size.x),
                _randomizerService.Range(-_arena.Size.y, _arena.Size.y)
            );
            Vector2 directionToRandomPoint = (randomPointInBounds - position).normalized;
            float angle = Mathf.Atan2(directionToRandomPoint.y, directionToRandomPoint.x) * Mathf.Rad2Deg;
            return angle;
        }
        
    }
}