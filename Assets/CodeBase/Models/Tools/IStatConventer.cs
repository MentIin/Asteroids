using System;
using System.Linq;
using System.Reflection;
using CodeBase.Models.Stats.Main;
using Unity.Plastic.Newtonsoft.Json;
using Unity.Plastic.Newtonsoft.Json.Linq;

namespace CodeBase.Models.Tools
{
    public class IStatConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(IStat).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            
            JObject jsonObject = JObject.Load(reader);
            
            // Если объект уже содержит свойства стата напрямую
            if (jsonObject.Properties().Any(p => p.Name.Equals("Value", StringComparison.OrdinalIgnoreCase)))
            {
                // Пытаемся найти тип по имени свойства objectType
                var typeName = objectType.Name;
                if (objectType.IsGenericType)
                {
                    typeName = objectType.GenericTypeArguments[0].Name;
                }
                
                var type = FindTypeByName(typeName);
                if (type == null) return null;
                
                var instance = Activator.CreateInstance(type);
                serializer.Populate(jsonObject.CreateReader(), instance);
                return instance;
            }
            
            // Получаем имя типа из JSON (первое свойство)
            var typeProperty = jsonObject.Properties().First();
            var typeNameFromJson = typeProperty.Name;
            
            // Находим соответствующий тип
            var typeFromJson = FindTypeByName(typeNameFromJson);
            if (typeFromJson == null)
                throw new JsonSerializationException($"Unknown type: {typeNameFromJson}");
            
            // Создаем экземпляр и заполняем его данными
            var instanceFromJson = Activator.CreateInstance(typeFromJson);
            serializer.Populate(typeProperty.Value.CreateReader(), instanceFromJson);
            
            return instanceFromJson;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var type = value.GetType();
            
            // Создаем временный сериализатор без этого конвертера, чтобы избежать рекурсии
            var tempSerializer = JsonSerializer.CreateDefault();
            tempSerializer.Formatting = serializer.Formatting;
            tempSerializer.NullValueHandling = serializer.NullValueHandling;
            tempSerializer.DefaultValueHandling = serializer.DefaultValueHandling;
            
            // Копируем все конвертеры кроме этого
            foreach (var converter in serializer.Converters)
            {
                if (converter.GetType() != this.GetType())
                {
                    tempSerializer.Converters.Add(converter);
                }
            }
            
            // Для вложенных объектов сериализуем напрямую
            if (IsNestedContext(writer))
            {
                var jObject = JObject.FromObject(value, tempSerializer);
                jObject.WriteTo(writer);
                return;
            }
            
            // Для корневых объектов используем обертку с именем типа
            var typeName = GetTypeName(type);
    
            writer.WriteStartObject();
            writer.WritePropertyName(typeName);
            
            // Создаем JObject для свойств
            var propsObject = new JObject();
            var properties = type.GetProperties()
                .Where(p => p.CanRead && p.GetCustomAttribute<JsonIgnoreAttribute>() == null);
    
            foreach (var prop in properties)
            {
                var propValue = prop.GetValue(value);
                if (propValue != null)
                {
                    propsObject.Add(ToCamelCase(prop.Name), JToken.FromObject(propValue, tempSerializer));
                }
            }
            
            propsObject.WriteTo(writer);
            writer.WriteEndObject();
        }

        private bool IsNestedContext(JsonWriter writer)
        {
            // Проверяем, находимся ли мы внутри словаря stats
            return !string.IsNullOrEmpty(writer.Path) && 
                   (writer.Path.Contains("stats") || writer.Path.Contains('.'));
        }

        private string ToCamelCase(string name)
        {
            if (string.IsNullOrEmpty(name)) return name;
            return char.ToLowerInvariant(name[0]) + name.Substring(1);
        }

        private string GetTypeName(Type type)
        {
            var typeNameAttr = type.GetCustomAttribute<JsonTypeNameAttribute>();
            return typeNameAttr?.Name ?? type.Name;
        }

        private Type FindTypeByName(string name)
        {
            // Ищем тип по атрибуту JsonTypeName
            var type = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .FirstOrDefault(t => 
                    t.GetCustomAttribute<JsonTypeNameAttribute>()?.Name == name && 
                    typeof(IStat).IsAssignableFrom(t));
        
            // Если не нашли по атрибуту, ищем по имени
            return type ?? AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .FirstOrDefault(t => t.Name == name && typeof(IStat).IsAssignableFrom(t));
        }
    }
}