using System.Collections.Generic;
using CodeBase.Data;
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

        private Dictionary<EnemyType, System.Type> _enemyTypes = new Dictionary<EnemyType, System.Type>
        {
            { EnemyType.BigAsteroid, typeof(BigAsteroid) },
            { EnemyType.SmallAsteroid, typeof(SmallAsteroid) },
            { EnemyType.Ufo, typeof(Ufo) }
        };

        public EnemyFactory(DiContainer container, IStaticDataService staticDataService, Arena arena)
        {
            _container = container;
            _staticDataService = staticDataService;
            _arena = arena;
        }

        public void SpawnEnemy(EnemyType type, Vector2 position)
        {
            EnemyConfig enemyConfig = _staticDataService.ForEnemy(type);
            object[] additionalArgs = new object[]{enemyConfig.Stats};
            Enemy component = _container.InstantiatePrefabResourceForComponent<Enemy>(
                enemyConfig.PrefabPath, position, Quaternion.identity, null, additionalArgs
            );
            
            if (component is IArenaMember member)
            {
                _arena.RegisterMember(member);
            }
        }
    }
}