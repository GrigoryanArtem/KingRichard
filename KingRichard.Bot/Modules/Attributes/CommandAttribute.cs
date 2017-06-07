using System;

namespace KingRichard.Bot.Modules.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CommandAttribute : Attribute
    {
        public string CommandName { get; set; }

        public CommandAttribute(string name)
        {
            CommandName = name;
        }
    }
}
