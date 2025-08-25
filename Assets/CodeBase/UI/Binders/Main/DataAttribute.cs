using System;

namespace CodeBase.UI.Binders.Main
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class DataAttribute : Attribute
    {
        public string Id { get; }

        public DataAttribute(string id)
        {
            Id = id;
        }
    }
}