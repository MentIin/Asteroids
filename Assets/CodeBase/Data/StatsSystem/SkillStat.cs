using CodeBase.Data.StaticData;
using CodeBase.Data.StatsSystem.Main;
using CodeBase.Data.Tools;

namespace CodeBase.Data.StatsSystem
{
    [JsonTypeName("skill")]
    public struct SkillStat : IStat
    {
        public float ReloadTime;
        public int MaxAmmo;
        public ProjectileType ProjectileType;
    }
}