using CodeBase.Interfaces.Infrastructure;
using CodeBase.Interfaces.Infrastructure.Services;
using CodeBase.Models.StaticData;

namespace CodeBase.Infrastructure.Services.StaticDataService
{
    public class StaticDataService : IStaticDataService
    {
        private readonly ILogService log;
        private IAssetProvider assetProvider;

        public StaticDataService(ILogService log, IAssetProvider assetProvider)
        {
            this.log = log;
            this.assetProvider = assetProvider;
        }

        public void Initialize()
        {
            
        }

        public PlayerStaticData ForPlayer()
        {
            //PlayerStaticData playerData = assetProvider.Load<PlayerStaticData>("StaticData/PlayerStaticData");

            PlayerStaticData playerData = new PlayerStaticData
            {
                PrefabPath = "Prefabs/Player/PlayerView"
            };
            
            return playerData;
        }
    }
}