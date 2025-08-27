using CodeBase.Data.StaticData;
using CodeBase.Data.StatsSystem.Main;
using CodeBase.Data.Tools;

namespace CodeBase.Data.StatsSystem
{
    [JsonTypeName("weapon")]
    public struct WeaponStat : IStat
    {
        public float ReloadTime;
        public ProjectileType ProjectileType;
    }
}