using CodeBase.Models.StaticData;

namespace CodeBase.Interfaces.Infrastructure.Services
{
    public interface IStaticDataService
    {
        PlayerStaticData ForPlayer();
    }
}