using CodeBase.Interfaces.Infrastructure.SceneManagement;
using CodeBase.Interfaces.Infrastructure.Services;
using Cysharp.Threading.Tasks;
using Zenject;

namespace CodeBase.Infrastructure
{
    public class GameBootstrapper : IInitializable
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IAnalyticsService _analyticsService;
        private readonly IAdsService _adsService;
        private readonly ILogService _logService;

        public GameBootstrapper(ISceneLoader sceneLoader, IAnalyticsService analyticsService,
            IAdsService adsService, ILogService logService)
        {
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
            await _analyticsService.Initialize();
            await _adsService.Initialize();
            await _sceneLoader.Load(SceneNames.GameplayScene);
        }
    }
}