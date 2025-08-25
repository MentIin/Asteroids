using CodeBase.Interfaces.Infrastructure.Services.UI;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Factories
{
    public class UIFactory : IUIFactory
    {
        private readonly DiContainer _container;

        public UIFactory(DiContainer container)
        {
            _container = container;
        }

        public void CreateHUD()
        {
            _container.InstantiatePrefabResource(UIFactoryAssets.HUD);
        }
    }
}