using CodeBase.Data.Stats.Main;
using CodeBase.Data.Tools;

namespace CodeBase.Data.Stats
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