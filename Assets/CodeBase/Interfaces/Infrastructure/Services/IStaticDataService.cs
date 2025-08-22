using CodeBase.Data;
using CodeBase.Data.Enums;
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
        ProjectileConfig ForProjectile(BulletType type);
    }
}