using CodeBase.Data;
using CodeBase.Data.StaticData;
using UnityEngine;

namespace CodeBase.Interfaces.Infrastructure.Services
{
    public interface IStaticDataService
    {
        void Initialize();
        PlayerConfig ForPlayer();
        EnemyConfig ForEnemy(EnemyType type);
        MapConfig ForMap();
    }
}