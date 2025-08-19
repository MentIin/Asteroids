using CodeBase.Interfaces.Infrastructure.SceneManagement;
using CodeBase.Interfaces.Infrastructure.Services;
using Cysharp.Threading.Tasks;
using Zenject;

namespace CodeBase.CompositionRoot.EntryPoints
{
    public class BootstrapEntryPoint : IInitializable
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IAnalyticsService _analyticsService;
        private readonly IAdsService _adsService;
        private readonly ILogService _logService;
        private readonly IStaticDataService _staticDataService;

        public BootstrapEntryPoint(ISceneLoader sceneLoader, IAnalyticsService analyticsService,
            IAdsService adsService, IStaticDataService staticDataService, ILogService logService)
        {
            _staticDataService = staticDataService;
            _analyticsService = analyticsService;
            _adsService = adsService;
            _sceneLoader = sceneLoader;
            _logService = logService;
        }

        public void Initialize()
        {
            SetUp();
        }

        private async UniTask SetUp()
        {
            _staticDataService.Initialize();
            await _analyticsService.Initialize();
            await _adsService.Initialize();
            await _sceneLoader.Load(SceneNames.GameplayScene);
        }
    }
}