using Discord.Commands;
using System;

namespace KingRichard.Bot.Modules.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ParameterAttribute : Attribute
    {
        public ParameterAttribute(string name, ParameterType type = ParameterType.Required)
        {
            ParameterName = name;
            ParameterType = type;
        }

        public string ParameterName { get; set; }

        public ParameterType ParameterType { get; set; }
    }
}
