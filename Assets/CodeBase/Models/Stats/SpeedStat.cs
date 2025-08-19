using CodeBase.Models.Stats.Main;
using CodeBase.Models.Tools;

namespace CodeBase.Models.Stats
{
    [JsonTypeName("speed")]
    public struct SpeedStat : IStat
    {
        public int Value;
        public SpeedStat(int value)
        {
            Value = value;
        }
    }
}