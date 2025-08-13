using System;
using System.Threading.Tasks;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.UI.LoadingCurtain;
using CodeBase.Services.AdsService;
using CodeBase.Services.AnalyticsService;
using CodeBase.Services.LogService;
using CodeBase.Services.StaticDataService;
using CodeBase.UI.Overlays;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.States
{
    public class GameBootstrapState : IState
    {
        private readonly GameStateMachine gameStateMachine;
        private readonly IAdsService adsService;
        private readonly StaticDataService staticDataService;
        private readonly IAnalyticsService analyticsService;
        private readonly ILogService log;
        private readonly LoadingCurtainProxy loadingCurtainProxy;
        private readonly AwaitingOverlayProxy awaitingOverlayProxy;
        private readonly IAssetProvider assetProvider;

        public GameBootstrapState(GameStateMachine gameStateMachine,
            IAdsService adsService,
            StaticDataService staticDataService,
            IAnalyticsService analyticsService,
            IAssetProvider assetProvider,
            ILogService log,
            LoadingCurtainProxy loadingCurtainProxy,
            AwaitingOverlayProxy awaitingOverlayProxy)
        {
            this.adsService = adsService;
            this.staticDataService = staticDataService;
            this.gameStateMachine = gameStateMachine;
            this.staticDataService = staticDataService;
            this.analyticsService = analyticsService;
            this.assetProvider = assetProvider;
            this.log = log;
            this.loadingCurtainProxy = loadingCurtainProxy;
            this.awaitingOverlayProxy = awaitingOverlayProxy;
        }

        public async UniTask Enter()
        {
            log.Log("BootstrapState Enter");
            
            await InitServices();
            
            log.Log("Starting GameLoadingState");
            gameStateMachine.Enter<GameLoadingState>().Forget();
        }

        private async UniTask InitServices()
        {
            // init global services that may need initialization in some order here

            staticDataService.Initialize();

            loadingCurtainProxy.InitializeAsync();
            awaitingOverlayProxy.InitializeAsync();
            
            analyticsService.Initialize();
            adsService.Initialize();
            
            log.Log("Services initialized");
        }

        public UniTask Exit() => 
            default;
    }
}