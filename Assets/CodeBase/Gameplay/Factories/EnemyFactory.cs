using CodeBase.Data.StaticData;
using CodeBase.Gameplay.Enemies;
using CodeBase.Gameplay.Enviroment;
using CodeBase.Interfaces.Infrastructure.Services;
using Zenject;

namespace CodeBase.Gameplay.Factories
{
    public class EnemyFactory
    {
        private readonly DiContainer _container;
        private readonly IStaticDataService _staticDataService;
        private readonly Arena _arena;
        private readonly IRandomizerService _randomizerService;

        public EnemyFactory(DiContainer container, IStaticDataService staticDataService, Arena arena,
            IRandomizerService randomizerService)
        {
            _container = container;
            _staticDataService = staticDataService;
            _arena = arena;
            _randomizerService = randomizerService;
        }

        public Enemy SpawnEnemy(EnemyType type)
        {
            EnemyConfig enemyConfig = _staticDataService.ForEnemy(type);
            object[] additionalArgs = new object[]{enemyConfig};
            
            Enemy component = _container.InstantiatePrefabResourceForComponent<Enemy>(
                enemyConfig.PrefabPath, additionalArgs
            );
            
            if (component is IArenaMember member)
            {
                _arena.RegisterMember(member);
            }

            return component;
        }
    }
}