using CodeBase.Gameplay.Player;
using CodeBase.Interfaces.Infrastructure.Services;
using CodeBase.Models;
using CodeBase.Models.StaticData;
using UnityEngine;
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
            PlayerStaticData playerData = _staticDataService.ForPlayer();
    
            
            PlayerModel model = new PlayerModel(Vector2.zero);
            PlayerView view = _container.InstantiatePrefabResourceForComponent<PlayerView>(
                playerData.PrefabPath
            );
            PlayerActor playerActor = _container.Instantiate<PlayerActor>(
                new object[] { model, view }
            );
            playerActor.Initialize();
        }
    }
}