using CodeBase.Interfaces.Infrastructure;
using CodeBase.Interfaces.Infrastructure.Services;
using CodeBase.Models.StaticData;

namespace CodeBase.Infrastructure.Services.StaticDataService
{
    public class StaticDataService : IStaticDataService
    {
        private readonly ILogService log;

        public StaticDataService(ILogService log)
        {
            this.log = log;
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