using System;

namespace KingRichard.Bot.Modules.Attributes
{
    public class PermissionAttribute : Attribute
    {
        public PermissionAttribute(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}
