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
            DiContainer subContainer = _container.CreateSubContainer();
            
            PlayerStaticData playerData = _staticDataService.ForPlayer();

            subContainer.Bind<PlayerModel>().ToSelf().AsSingle().WithArguments(Vector2.zero);
            subContainer.Bind<PlayerView>().FromComponentInNewPrefabResource(playerData.PrefabPath)
                .AsSingle().NonLazy();
            subContainer.BindInterfacesAndSelfTo<PlayerActor>().AsSingle();
        }
    }
}