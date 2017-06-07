using System;

namespace KingRichard.Bot.Modules.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class DescriptionAttribute : Attribute
    {
        public string DescriptionText { get; set; }

        public DescriptionAttribute(string descriptionText)
        {
            DescriptionText = descriptionText;
        }
    }
}
