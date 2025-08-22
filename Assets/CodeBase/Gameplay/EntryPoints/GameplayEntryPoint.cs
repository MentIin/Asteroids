using CodeBase.Gameplay.Enviroment;
using CodeBase.Gameplay.Factories;
using CodeBase.Gameplay.Player;
using CodeBase.Gameplay.Services.CameraService;
using CodeBase.Gameplay.Services.SpawnService;
using Zenject;

namespace CodeBase.Gameplay.EntryPoints
{
    public class GameplayEntryPoint : IInitializable
    {
        private readonly PlayerFactory _playerFactory;
        private readonly Arena _arena;
        private readonly CameraService _cameraService;
        private readonly EnemySpawnService _enemySpawnService;
        private readonly PlayerProvider _playerProvider;

        public GameplayEntryPoint(PlayerFactory playerFactory,
            Arena arena, CameraService cameraService,
            EnemySpawnService enemySpawnService, PlayerProvider playerProvider)
        {
            _playerFactory = playerFactory;
            _arena = arena;
            _cameraService = cameraService;
            _enemySpawnService = enemySpawnService;
            _playerProvider = playerProvider;
        }
        public void Initialize()
        {
            Player.Player player = _playerFactory.CreatePlayer();
            _arena.Initialize();
            _cameraService.Follow(player.transform);
            
            _enemySpawnService.StartSpawn();
        }
    }
}