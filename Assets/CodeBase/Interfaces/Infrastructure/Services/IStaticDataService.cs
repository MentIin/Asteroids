using CodeBase.Models;
using CodeBase.Models.StaticData;

namespace CodeBase.Interfaces.Infrastructure.Services
{
    public interface IStaticDataService
    {
        PlayerConfig ForPlayer();
        EnemyData ForEnemy(EnemyType type);
        void Initialize();
    }
}