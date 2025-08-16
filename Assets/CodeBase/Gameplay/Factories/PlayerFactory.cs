using CodeBase.Interfaces.Infrastructure;
using CodeBase.Interfaces.Infrastructure.Services;

namespace CodeBase.Gameplay.Factories
{
    public class PlayerFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticDataService;

        public PlayerFactory(IAssetProvider assetProvider, IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
            _assetProvider = assetProvider;
        }
    }
}