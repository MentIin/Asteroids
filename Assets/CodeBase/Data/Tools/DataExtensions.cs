using System.Collections.Generic;
using Unity.Plastic.Newtonsoft.Json;
using Unity.Plastic.Newtonsoft.Json.Converters;
using Unity.Plastic.Newtonsoft.Json.Serialization;

namespace CodeBase.Data.Tools
{
    public static class DataExtensions
    {
        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            Converters = new List<JsonConverter>
            {
                new StringEnumConverter(),
                new IStatConverter() 
            },
            Formatting = Formatting.Indented, // For better readability
            MissingMemberHandling = MissingMemberHandling.Ignore,
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public static string ToJson(this object obj) => 
            JsonConvert.SerializeObject(obj, Settings);

        public static T ToDeserialized<T>(this string json) =>
            JsonConvert.DeserializeObject<T>(json, Settings);
    }
}