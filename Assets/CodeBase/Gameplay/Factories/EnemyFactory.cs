using CodeBase.Data;
using CodeBase.Gameplay.Enemies;
using CodeBase.Gameplay.Enemies.Asteroids;
using CodeBase.Gameplay.Enemies.Asteroids.Big;
using CodeBase.Gameplay.Enemies.Asteroids.Small;
using CodeBase.Gameplay.Enemies.Ufo;
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

        public EnemyFactory(DiContainer container, IStaticDataService staticDataService)
        {
            _container = container;
            _staticDataService = staticDataService;
        }

        public void SpawnEnemy(EnemyType type, Vector2 position)
        {
            object[] additionalArgs;
            EnemyData enemyData = _staticDataService.ForEnemy(type);
            switch (enemyData.Type)
            {
                case EnemyType.BigAsteroid:
                    BigAsteroidModel model = _container.Instantiate<BigAsteroidModel>(
                        new object[]{enemyData.Stats}
                        );
                    additionalArgs = new object[]{model};
                    break;
                case EnemyType.SmallAsteroid:
                    SmallAsteroidModel smallModel = _container.Instantiate<SmallAsteroidModel>(
                        new object[]{enemyData.Stats}
                        );
                    additionalArgs = new object[]{smallModel};
                    break;
                case EnemyType.Ufo:
                    UfoModel ufoModel = _container.Instantiate<UfoModel>(
                        new object[]{enemyData.Stats}
                        );
                    additionalArgs = new object[]{ufoModel};
                    break;
                default:
                    return;
            }

            _container.InstantiatePrefabResourceForComponent<Enemy>(
                enemyData.PrefabPath, position, Quaternion.identity, null, additionalArgs
                );
        }
    }
}