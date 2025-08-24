using System.Collections.Generic;
using CodeBase.Data;
using CodeBase.Data.Enums;
using CodeBase.Data.StaticData;
using CodeBase.Gameplay.Enemies;
using CodeBase.Gameplay.Enemies.Asteroids;
using CodeBase.Gameplay.Enemies.Asteroids.Big;
using CodeBase.Gameplay.Enemies.Asteroids.Small;
using CodeBase.Gameplay.Enemies.Ufo;
using CodeBase.Gameplay.Enviroment;
using CodeBase.Interfaces.Infrastructure;
using CodeBase.Interfaces.Infrastructure.Services;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Factories
{
    public class EnemyFactory
    {
        private readonly DiContainer _container;
        private readonly IStaticDataService _staticDataService;
        private readonly Arena _arena;
        private readonly IRandomizerService _randomizerService;

        private Dictionary<EnemyType, System.Type> _enemyTypes = new Dictionary<EnemyType, System.Type>
        {
            { EnemyType.BigAsteroid, typeof(BigAsteroid) },
            { EnemyType.SmallAsteroid, typeof(SmallAsteroid) },
            { EnemyType.Ufo, typeof(Ufo) }
        };

        public EnemyFactory(DiContainer container, IStaticDataService staticDataService, Arena arena,
            IRandomizerService randomizerService)
        {
            _container = container;
            _staticDataService = staticDataService;
            _arena = arena;
            _randomizerService = randomizerService;
        }

        public void SpawnEnemy(EnemyType type, Vector2 position)
        {
            EnemyConfig enemyConfig = _staticDataService.ForEnemy(type);
            object[] additionalArgs = new object[]{enemyConfig.Stats};
            
            Quaternion rotation = GetRandomRotation(position);
            Enemy component = _container.InstantiatePrefabResourceForComponent<Enemy>(
                enemyConfig.PrefabPath, position, rotation, null, additionalArgs
            );
            
            if (component is IArenaMember member)
            {
                _arena.RegisterMember(member);
            }
        }

        private Quaternion GetRandomRotation(Vector2 position)
        {
            Vector2 randomPointInBounds = new Vector2(
                _randomizerService.Range(-_arena.Size.x, _arena.Size.x),
                _randomizerService.Range(-_arena.Size.y, _arena.Size.y)
            );
            Vector2 directionToRandomPoint = (randomPointInBounds - position).normalized;
            float angle = Mathf.Atan2(directionToRandomPoint.y, directionToRandomPoint.x) * Mathf.Rad2Deg;
            return Quaternion.Euler(0, 0, angle);
        }
    }
}