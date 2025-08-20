using CodeBase.Data;
using CodeBase.Data.StaticData;

namespace CodeBase.Interfaces.Infrastructure.Services
{
    public interface IStaticDataService
    {
        PlayerConfig ForPlayer();
        EnemyData ForEnemy(EnemyType type);
        void Initialize();
    }
}