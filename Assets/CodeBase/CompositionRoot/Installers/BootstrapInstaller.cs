using CodeBase.CompositionRoot.EntryPoints;
using CodeBase.Infrastructure.SceneManagement;
using CodeBase.Infrastructure.Services.AdsService;
using CodeBase.Infrastructure.Services.AnalyticsService;
using CodeBase.Infrastructure.Services.Camera;
using CodeBase.Infrastructure.Services.LogService;
using CodeBase.Infrastructure.Services.PlayerProgressService;
using CodeBase.Infrastructure.Services.RandomizerService;
using CodeBase.Infrastructure.Services.StaticDataService;
using CodeBase.Interfaces.Infrastructure.Services;
using CodeBase.UI.Services.Factories;
using UnityEngine;
using Zenject;

namespace CodeBase.CompositionRoot.Installers
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

            Container.BindInterfacesAndSelfTo<CameraService>().AsSingle();
        }


        private void BindGameBootstrapper()
        {
            Container.BindInterfacesAndSelfTo<BootstrapEntryPoint>()
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
    }
}