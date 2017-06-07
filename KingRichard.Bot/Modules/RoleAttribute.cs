using System;

namespace KingRichard.Bot.Modules
{
    [AttributeUsage(AttributeTargets.Method)]
    public class RoleAttribute : Attribute
    {
        public BotRoles AvailableRole { get; set; }

        public RoleAttribute(BotRoles availableRole)
        {
            AvailableRole = availableRole;
        }
    }
}
