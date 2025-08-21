using CodeBase.Data.StaticData;
using CodeBase.Gameplay.Enviroment;
using CodeBase.Gameplay.Player;
using CodeBase.Interfaces.Infrastructure.Services;
using Zenject;

namespace CodeBase.Gameplay.Factories
{
    public class PlayerFactory
    {
        private readonly IStaticDataService _staticDataService;
        private readonly Arena _arena;
        private readonly PlayerProvider _playerProvider;
        private readonly DiContainer _container;

        public PlayerFactory(DiContainer container,
            IStaticDataService staticDataService, Arena arena, PlayerProvider playerProvider)
        {
            _container = container;
            _staticDataService = staticDataService;
            _arena = arena;
            _playerProvider = playerProvider;
        }

        public Player.Player CreatePlayer()
        {
            PlayerConfig playerData = _staticDataService.ForPlayer();
            Player.Player player = _container.InstantiatePrefabResourceForComponent<Player.Player>(
                playerData.PrefabPath, new object[]{playerData.Stats}
                );
            
            
            _arena.RegisterMember(player);
            _playerProvider.RegisterPlayer(player);
            return player;
        }
    }
}