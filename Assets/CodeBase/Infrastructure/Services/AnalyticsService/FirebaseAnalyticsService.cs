using CodeBase.Interfaces.Infrastructure.Services;
using Cysharp.Threading.Tasks;

namespace CodeBase.Infrastructure.Services.AnalyticsService
{
    public class FirebaseAnalyticsService : IAnalyticsService
    {
        public UniTask Initialize()
        {
            
            return UniTask.CompletedTask;
        }
    }
}