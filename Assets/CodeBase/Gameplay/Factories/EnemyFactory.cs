using CodeBase.Data;
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

        public GameObject SpawnEnemy(EnemyType type, Vector2 position)
        {
            _staticDataService.ForEnemy(type);
            return null;
        }
    }
}