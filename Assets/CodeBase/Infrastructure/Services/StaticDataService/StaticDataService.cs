using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.AssetManagement.Services;
using CodeBase.Services.ServerConnectionService;

namespace CodeBase.Services.StaticDataService
{
    public class StaticDataService
    {
        public ServerConnectionConfig ServerConnectionConfig { get; private set; }
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