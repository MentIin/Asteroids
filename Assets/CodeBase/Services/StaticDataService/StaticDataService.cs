using System.Collections.Generic;
using System.Linq;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Services.AdsService;
using CodeBase.Services.LogService;
using CodeBase.Services.ServerConnectionService;
using CodeBase.UI.PopUps.PolicyAcceptPopup;
using Cysharp.Threading.Tasks;

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