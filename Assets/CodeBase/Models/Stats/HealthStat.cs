using CodeBase.Models.Stats.Main;
using CodeBase.Models.Tools;

namespace CodeBase.Models.Stats
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