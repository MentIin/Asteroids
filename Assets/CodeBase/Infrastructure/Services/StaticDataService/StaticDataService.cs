using CodeBase.Interfaces.Infrastructure;
using CodeBase.Interfaces.Infrastructure.Services;

namespace CodeBase.Infrastructure.Services.StaticDataService
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