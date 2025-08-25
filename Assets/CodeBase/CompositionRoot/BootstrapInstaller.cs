using CodeBase.Infrastructure.SceneManagement;
using CodeBase.Infrastructure.Services.AdsService;
using CodeBase.Infrastructure.Services.AnalyticsService;
using CodeBase.Infrastructure.Services.Camera;
using CodeBase.Infrastructure.Services.InputService;
using CodeBase.Infrastructure.Services.LogService;
using CodeBase.Infrastructure.Services.PlayerProgressService;
using CodeBase.Infrastructure.Services.RandomizerService;
using CodeBase.Infrastructure.Services.StaticDataService;
using CodeBase.Interfaces.Infrastructure.Services;
using CodeBase.Interfaces.Infrastructure.Services.UI;
using CodeBase.UI.Factories;
using CodeBase.UI.ViewModels;
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
            
            BindPlayerScoreService();
            
            BindInputService();

            BindAdsService();

            BindAnalyticsService();
            
            BindUIFactory();
            
            BindViewModels();
            
            BindGameBootstrapper();
        }

        private void BindViewModels()
        {
            Container.BindInterfacesAndSelfTo<ScoreViewModel>().AsSingle().NonLazy();
        }

        private void BindLogService()
        {
            Container.Bind<ILogService>().To<LogService>().AsSingle();
        }
        

        private void BindInputService() =>
            Container.Bind<IInputService>().To<PCInputService>().AsSingle();

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
        

        private void BindUIFactory() =>
            Container.Bind<IUIFactory>().To<UIFactory>().AsSingle();

        private void BindRandomizeService() => 
            Container.BindInterfacesAndSelfTo<RandomizerService>().AsSingle();

        private void BindPlayerScoreService()
        {
            Container
                .Bind<IScoreService>().To<ScoreService>().AsSingle();
        }

        private void BindAdsService() => 
            Container.BindInterfacesAndSelfTo<AdsService>().AsSingle();

        private void BindAnalyticsService() => 
            Container.BindInterfacesTo<AnalyticsService>().AsSingle();
    }
}