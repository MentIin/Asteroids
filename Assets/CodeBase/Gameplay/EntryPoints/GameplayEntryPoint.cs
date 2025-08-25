using CodeBase.Gameplay.Enviroment;
using CodeBase.Gameplay.Factories;
using CodeBase.Gameplay.Player;
using CodeBase.Gameplay.Services.SpawnService;
using CodeBase.Interfaces.Infrastructure.Services;
using CodeBase.Interfaces.Infrastructure.Services.UI;
using Zenject;

namespace CodeBase.Gameplay.EntryPoints
{
    public class GameplayEntryPoint : IInitializable
    {
        private readonly PlayerFactory _playerFactory;
        private readonly Arena _arena;
        private readonly ICameraService _cameraService;
        private readonly EnemySpawnService _enemySpawnService;
        private readonly IUIFactory _uiFactory;

        public GameplayEntryPoint(PlayerFactory playerFactory,
            Arena arena, ICameraService cameraService,
            EnemySpawnService enemySpawnService, IUIFactory uiFactory)
        {
            _playerFactory = playerFactory;
            _arena = arena;
            _cameraService = cameraService;
            _enemySpawnService = enemySpawnService;
            _uiFactory = uiFactory;
        }
        public void Initialize()
        {
            Player.Player player = _playerFactory.CreatePlayer();
            _arena.Initialize();
            _cameraService.Follow(player.transform);
            
            _enemySpawnService.StartSpawn();
            
            _uiFactory.CreateHUD();
        }
    }
}