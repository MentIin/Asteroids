using CodeBase.Data.StaticData;
using CodeBase.Gameplay.Player;
using CodeBase.Interfaces.Infrastructure.Services;
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

        public Player.Player CreatePlayer()
        {
            PlayerConfig playerData = _staticDataService.ForPlayer();

            PlayerModel playerModel = _container.Instantiate<PlayerModel>();
            Player.Player player = _container.InstantiatePrefabResourceForComponent<Player.Player>(
                playerData.PrefabPath, new object[]{playerModel}
                );
            //_container.InstantiatePrefabResourceForComponent<Player>(playerData.PrefabPath);
            return player;
        }
    }
}