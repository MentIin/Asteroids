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
        private readonly SpawnService _spawnService;

        public GameplayEntryPoint(PlayerFactory playerFactory,
            Arena arena, CameraService cameraService,
            SpawnService spawnService)
        {
            _playerFactory = playerFactory;
            _arena = arena;
            _cameraService = cameraService;
            _spawnService = spawnService;
        }
        public void Initialize()
        {
            Player.Player player = _playerFactory.CreatePlayer();
            _arena.Initialize(player.TransformData);
            _cameraService.Follow(player.transform);
            
            _spawnService.StartSpawn();
        }
    }
}