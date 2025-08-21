using CodeBase.Gameplay.EntryPoints;
using CodeBase.Gameplay.Enviroment;
using CodeBase.Gameplay.Factories;
using CodeBase.Gameplay.Player;
using CodeBase.Gameplay.Services.SpawnService;
using Zenject;

namespace CodeBase.Gameplay.Installers
{
    public class GameplaySceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindFactories();

            BindSpawnService();

            BindArena();
            
            BindPlayerProvider();
            
            BindEntryPoint();
        }

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
            Container.BindInterfacesAndSelfTo<Arena>().AsSingle();
        }

        private void BindFactories()
        {
            Container.Bind<PlayerFactory>().ToSelf().AsSingle();
            Container.Bind<EnemyFactory>().ToSelf().AsSingle();
            Container.Bind<SpawnerFactory>().ToSelf().AsSingle();
        }

        private void BindEntryPoint()
        {
            Container.BindInterfacesAndSelfTo<GameplayEntryPoint>()
                .AsSingle()
                .NonLazy();
        }
    }
}