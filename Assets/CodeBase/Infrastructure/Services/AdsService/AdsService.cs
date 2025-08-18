using System;
using CodeBase.Interfaces.Infrastructure.Services;
using Cysharp.Threading.Tasks;

namespace CodeBase.Infrastructure.Services.AdsService
{
    public class AdsService : IAdsService
    {
        public event Action RewardedVideoReady;

        public bool IsRewardedVideoReady { get; }

        private readonly ILogService log;

        public AdsService(ILogService log)
        {
            this.log = log;
        }

        public UniTask Initialize()
        {
            log.LogWarning("Initialization of ads service isn't implemented yet");
            return UniTask.CompletedTask;
        }

        public void ShowRewardedVideo(Action onVideoFinished)
        {
            log.LogWarning("Showing of ads isn't implemented yet");
        }
    }
}