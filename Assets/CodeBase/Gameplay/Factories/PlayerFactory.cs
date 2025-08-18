using CodeBase.Gameplay.Player;
using CodeBase.Interfaces.Infrastructure;
using CodeBase.Interfaces.Infrastructure.Services;
using CodeBase.Models;
using CodeBase.Models.StaticData;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Factories
{
    public class PlayerFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticDataService;
        private readonly DiContainer _container;

        public PlayerFactory(DiContainer container, IAssetProvider assetProvider,
            IStaticDataService staticDataService)
        {
            _container = container;
            _staticDataService = staticDataService;
            _assetProvider = assetProvider;
        }

        public PlayerActor CreatePlayer()
        {
            DiContainer subContainer = _container.CreateSubContainer();
            
            PlayerStaticData playerData = _staticDataService.ForPlayer();

            subContainer.Bind<PlayerModel>().ToSelf().AsSingle().WithArguments(Vector2.zero);
            
            PlayerView view = _assetProvider.Instantiate<PlayerView>(playerData.PrefabPath);
            
            PlayerActor player = _container.Instantiate<PlayerActor>((new object[] { model, view}));
            
            
            return player;
        }
    }
}