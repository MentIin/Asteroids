using CodeBase.CompositionRoot.EntryPoints;
using CodeBase.Interfaces.Infrastructure.Services.UI;
using CodeBase.UI.Factories;
using Zenject;

namespace CodeBase.CompositionRoot.Installers
{
    public class MainMenuSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindUIFactory();
            BindEntryPoint();
        }

        private void BindUIFactory() =>
            Container.Bind<IUIFactory>().To<UIFactory>().AsSingle();
        
        private void BindEntryPoint()
        {
            Container.BindInterfacesAndSelfTo<MainMenuEntryPoint>()
                .AsSingle()
                .NonLazy();
        }
    }
}