using CodeBase.Data.Stats.Main;
using CodeBase.Data.Tools;

namespace CodeBase.Data.Stats
{
    [JsonTypeName("health")]
    public struct HealthStat : IStat
    {
        public int Value;
        public HealthStat(int value)
        {
            Value = value;
        }
    }
}