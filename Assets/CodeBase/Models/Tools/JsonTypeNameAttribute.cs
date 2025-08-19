using System;

namespace CodeBase.Models.Tools
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class JsonTypeNameAttribute : Attribute
    {
        public string Name { get; }

        public JsonTypeNameAttribute(string name)
        {
            Name = name;
        }
    }
}