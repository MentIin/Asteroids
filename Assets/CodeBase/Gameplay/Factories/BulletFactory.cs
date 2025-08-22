using CodeBase.Data.Enums;
using CodeBase.Data.StaticData;
using CodeBase.Interfaces.Infrastructure.Services;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Factories
{
    public class BulletFactory
    {
        private readonly DiContainer _container;
        private readonly IStaticDataService _staticDataService;

        public BulletFactory(DiContainer container, IStaticDataService staticDataService)
        {
            _container = container;
            _staticDataService = staticDataService;
        }
        
        
        public void CreateProjectile(BulletType type, Vector2 position, Quaternion rotation)
        {
            ProjectileConfig config = _staticDataService.ForProjectile(type);
            
            
            _container.InstantiatePrefabResourceForComponent<MonoBehaviour>(
                config.PrefabPath, position, rotation, null,
                new object[] {config.Stats}
                    );
        }
    }
}