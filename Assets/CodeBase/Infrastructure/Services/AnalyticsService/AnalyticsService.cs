using CodeBase.Interfaces.Infrastructure.Services;
using Cysharp.Threading.Tasks;

namespace CodeBase.Infrastructure.Services.AnalyticsService
{
    public class AnalyticsService : IAnalyticsService
    {
        public UniTask Initialize()
        {
            
            // initialize analytics service here
            
            return UniTask.CompletedTask;
        }
    }
}