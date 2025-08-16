using System;
using Cysharp.Threading.Tasks;

namespace CodeBase.Interfaces.Infrastructure.Services
{
    public interface IAdsService
    {
        event Action RewardedVideoReady;
        bool IsRewardedVideoReady { get; }
        UniTask Initialize();
        void ShowRewardedVideo(Action onVideoFinished);
    }
}