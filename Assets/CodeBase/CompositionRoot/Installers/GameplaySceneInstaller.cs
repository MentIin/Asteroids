using CodeBase.CompositionRoot.EntryPoints;
using CodeBase.Gameplay.Enviroment;
using CodeBase.Gameplay.Factories;
using CodeBase.Gameplay.Services.Providers;
using CodeBase.Gameplay.Services.SpawnService;
using CodeBase.Gameplay.ViewModels;
using CodeBase.Infrastructure.Services.InputService;
using CodeBase.Interfaces.Infrastructure.Services.UI;
using CodeBase.UI.Controls;
using CodeBase.UI.Factories;
using Zenject;

namespace CodeBase.CompositionRoot.Installers
{
    public class GameplaySceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindGameFactories();
            
            BindUIFactory();

            BindSpawnService();

            BindArena();
            
            BindPlayerProvider();

            BindViewModels();
            
            BindEntryPoint();
        }

        private void BindViewModels()
        {
            Container.BindInterfacesAndSelfTo<PlayerViewModel>().AsSingle().NonLazy();
        }
        private void BindUIFactory() =>
            Container.Bind<IUIFactory>().To<UIFactory>().AsSingle();
        
        private void BindPlayerProvider()
        {
            Container.Bind<PlayerProvider>().To<PlayerProvider>().AsSingle();
        }

        private void BindSpawnService()
        {
            Container.Bind<EnemySpawnService>().To<EnemySpawnService>().AsSingle();
        }

        private void BindArena()
        {
            Container.BindTickableExecutionOrder<Arena>(-100);
            Container.BindInterfacesAndSelfTo<Arena>().AsSingle();
        }

        private void BindGameFactories()
        {
            Container.Bind<PlayerFactory>().ToSelf().AsSingle();
            Container.Bind<EnemyFactory>().ToSelf().AsSingle();
            Container.Bind<SpawnerFactory>().ToSelf().AsSingle();
            Container.Bind<ProjectileFactory>().ToSelf().AsSingle();
        }

        private void BindEntryPoint()
        {
            Container.BindInterfacesAndSelfTo<GameplayEntryPoint>()
                .AsSingle()
                .NonLazy();
        }
        
    }
}