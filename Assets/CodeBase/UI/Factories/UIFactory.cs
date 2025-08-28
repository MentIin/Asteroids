using CodeBase.Interfaces.Infrastructure.Services.UI;
using CodeBase.UI.Controls;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Factories
{
    public class UIFactory : IUIFactory
    {
        private readonly DiContainer _container;
        private readonly IMobileInputProvider _mobileInputProvider;

        public UIFactory(DiContainer container, IMobileInputProvider mobileInputProvider)
        {
            _container = container;
            _mobileInputProvider = mobileInputProvider;
        }

        public void CreateHUD()
        {
            GameObject gameObject = _container.InstantiatePrefabResource(UIFactoryAssets.HUD);
            _mobileInputProvider.Register(gameObject.GetComponentInChildren<MobileInput>());
        }
    }
}