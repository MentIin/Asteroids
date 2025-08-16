using CodeBase.Interfaces.Infrastructure;
using CodeBase.Interfaces.Infrastructure.Services;

namespace CodeBase.Gameplay.Factories
{
    public class EnemyFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly ILogService _log;

        public EnemyFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }
    }
}