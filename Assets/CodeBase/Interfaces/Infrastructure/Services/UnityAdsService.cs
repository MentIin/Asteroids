using System;
using Cysharp.Threading.Tasks;

namespace CodeBase.Interfaces.Infrastructure.Services
{
    public class UnityAdsService : IAdsService, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        public event Action RewardedVideoReady;
        public bool IsRewardedVideoReady { get; }
        public UniTask Initialize()
        {
            
        }

        public void ShowRewardedVideo(Action onVideoFinished)
        {
            throw new NotImplementedException();
        }
    }
}