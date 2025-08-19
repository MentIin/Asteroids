using CodeBase.Gameplay.Player;
using CodeBase.Interfaces.Infrastructure.Services;
using CodeBase.Models.StaticData;
using Zenject;

namespace CodeBase.Gameplay.Factories
{
    public class PlayerFactory
    {
        private readonly IStaticDataService _staticDataService;
        private readonly DiContainer _container;

        public PlayerFactory(DiContainer container,
            IStaticDataService staticDataService)
        {
            _container = container;
            _staticDataService = staticDataService;
        }

        public void CreatePlayer()
        {
            PlayerConfig playerData = _staticDataService.ForPlayer();


            _container.InstantiatePrefabResourceForComponent<PlayerController>(playerData.PrefabPath);
        }
    }
}