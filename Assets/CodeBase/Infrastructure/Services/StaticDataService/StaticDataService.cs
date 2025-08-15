using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.AssetManagement.Services;

namespace CodeBase.Services.StaticDataService
{
    public class StaticDataService
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
        
    }
}