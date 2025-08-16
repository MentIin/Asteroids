using CodeBase.Infrastructure;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.CameraService;
using CodeBase.Infrastructure.SceneManagement;
using CodeBase.Infrastructure.Services.AnalyticsService;
using CodeBase.Infrastructure.Services.LogService;
using CodeBase.Interfaces.Infrastructure.Services;
using CodeBase.Services.AdsService;
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
        [SerializeField] private GameObject _cameraPrefab;
        public override void InstallBindings()
        {
            BindLogService();
            
            BindCamera();
            
            BindSceneLoader();

            BindStaticDataService();

            BindRandomizeService();
            
            BindInputService();

            BindAssetProvider();
            
            BindPlayerProgressService();

            BindAdsService();

            BindAnalyticsService();
            
            BindUIFactory();
            
            BindGameBootstrapper();
        }

        private void BindLogService()
        {
            Container.Bind<ILogService>().To<LogService>().AsSingle();
        }

        private void BindCamera()
        {
            Container.Bind<Camera>().FromComponentInNewPrefab(_cameraPrefab)
                .AsSingle()
                .NonLazy();
            
            Container.Bind<CameraService>().To<CameraService>().AsSingle();
        }


        private void BindGameBootstrapper()
        {
            Container.BindInterfacesAndSelfTo<GameBootstrapper>()
                .AsSingle()
                .NonLazy();
        }

        private void BindSceneLoader() => 
            Container.BindInterfacesAndSelfTo<SceneLoader>().AsSingle();

        private void BindStaticDataService() => 
            Container.BindInterfacesAndSelfTo<StaticDataService>().AsSingle();
        

        private void BindUIFactory()
        {
            Container
                .Bind<UIFactory>()
                .FromSubContainerResolve()
                .ByInstaller<UIFactoryInstaller>()
                .AsSingle();
        }

        private void BindRandomizeService() => 
            Container.BindInterfacesAndSelfTo<RandomizerService>().AsSingle();

        private void BindPlayerProgressService()
        {
            Container
                .BindInterfacesAndSelfTo<PersistentProgressService>()
                .AsSingle();
        }

        private void BindAdsService() => 
            Container.BindInterfacesAndSelfTo<AdsService>().AsSingle();

        private void BindAnalyticsService() => 
            Container.BindInterfacesTo<AnalyticsService>().AsSingle();

        private void BindInputService() => 
            Container.BindInterfacesAndSelfTo<InputService>().AsSingle();

        private void BindAssetProvider() => 
            Container.BindInterfacesTo<AssetProvider>().AsSingle();
    }
}