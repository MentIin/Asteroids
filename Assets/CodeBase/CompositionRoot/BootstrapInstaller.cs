using CodeBase.Infrastructure;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.CameraService;
using CodeBase.Infrastructure.Factories;
using CodeBase.Infrastructure.SceneManagement;
using CodeBase.Interfaces.Infrastructure.Factories;
using CodeBase.Services.AdsService;
using CodeBase.Services.AnalyticsService;
using CodeBase.Services.InputService;
using CodeBase.Services.PlayerProgressService;
using CodeBase.Services.RandomizerService;
using CodeBase.Services.StaticDataService;
using CodeBase.UI.Services.Factories;
using UnityEngine;
using Zenject;

namespace CodeBase.CompositionRoot
{
    public class BootstrapInstaller : MonoInstaller
    {
        
        [SerializeField] private Camera _camera;
        public override void InstallBindings()
        {
            BindCamera();

            BindGameBootstrapperFactory();

            BindSceneLoader();

            BindStaticDataService();

            BindGameFactory();
            
            BindUIFactory();

            BindRandomizeService();

            BindPlayerProgressService();

            BindAdsService();

            BindAnalyticsService();
            
            BindInputService();

            BindAssetProvider();
        }

        private void BindCamera()
        {
            Container.Bind<Camera>().FromInstance(_camera).AsSingle();
            Container.Bind<CameraService>().To<CameraService>().AsSingle();
        }


        private void BindAssetProvider() => 
            Container.BindInterfacesTo<AssetProvider>().AsSingle();

        private void BindAnalyticsService() => 
            Container.BindInterfacesTo<AnalyticsService>().AsSingle();

        private void BindStaticDataService() => 
            Container.BindInterfacesAndSelfTo<StaticDataService>().AsSingle();

        private void BindGameBootstrapperFactory()
        {
            Container
                .BindFactory<GameBootstrapper, GameBootstrapper.Factory>()
                .FromComponentInNewPrefabResource(InfrastructureAssetPath.GameBootstrapper);
        }

        private void BindInputService() => 
            Container.BindInterfacesAndSelfTo<InputService>().AsSingle();

        private void BindAdsService() => 
            Container.BindInterfacesAndSelfTo<AdsService>().AsSingle();
        

        private void BindPlayerProgressService()
        {
            Container
                .BindInterfacesAndSelfTo<PersistentProgressService>()
                .AsSingle();
        }

        private void BindRandomizeService() => 
            Container.BindInterfacesAndSelfTo<RandomizerService>().AsSingle();

        private void BindGameFactory()
        {
            Container
                .Bind<IGameFactory>()
                .FromSubContainerResolve()
                .ByInstaller<GameFactoryInstaller>()
                .AsSingle();
        }

        private void BindUIFactory()
        {
            Container
                .Bind<UIFactory>()
                .FromSubContainerResolve()
                .ByInstaller<UIFactoryInstaller>()
                .AsSingle();
        }
        

        private void BindSceneLoader() => 
            Container.BindInterfacesAndSelfTo<SceneLoader>().AsSingle();
        
    }
}