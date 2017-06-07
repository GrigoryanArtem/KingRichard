using KingRichard.Bot.Modules;

namespace KingRichard.Bot
{
    internal class BotConstants
    {
        public const string DefaultStyle = "Markdown";

        internal static int Delay
        {
            get
            {
                return 5000;
            }
        }

        internal static char DefaultPrefixChar
        {
            get
            {
                return '-';
            }
        }
        internal static BotRoles DeafultUserRole
        {
            get
            {
                return BotRoles.User;
            }
        }

        internal static IModule EmptyParent
        {
            get
            {
                return null;
            }
        }
    }
}
