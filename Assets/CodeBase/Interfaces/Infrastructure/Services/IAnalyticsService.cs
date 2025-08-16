using Cysharp.Threading.Tasks;

namespace CodeBase.Interfaces.Infrastructure.Services
{
    public interface IAnalyticsService
    {
        UniTask Initialize();
    }
}