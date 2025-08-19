using System;
using Unity.Plastic.Newtonsoft.Json;
using Unity.Plastic.Newtonsoft.Json.Converters;

namespace CodeBase.Models
{
    [Serializable]
    public struct EnemyData
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public EnemyType Type;
        public string PrefabPath;
        public Stats.Main.Stats Stats;
    }
}