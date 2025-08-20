using CodeBase.Gameplay.EntryPoints;
using CodeBase.Gameplay.Enviroment;
using CodeBase.Gameplay.Factories;
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
            
            BindEntryPoint();
        }

        private void BindSpawnService()
        {
            Container.Bind<SpawnService>().To<SpawnService>().AsSingle();
        }

        private void BindArena()
        {
            Container.Bind<Arena>().ToSelf().AsSingle();
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