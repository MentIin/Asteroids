using CodeBase.Interfaces.Infrastructure;

namespace CodeBase.Gameplay.Factories
{
    public class EnemyFactory
    {
        private readonly IAssetProvider _assetProvider;

        public EnemyFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }
    }
}