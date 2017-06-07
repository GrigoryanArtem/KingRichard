namespace KingRichard.Bot.Modules.Moderation
{
    internal class Constants
    {
        public const string MuteRoleName = "Mute";

        public const string ModerationModuleName = "Moderation";
        public const string ModerationModuleDescription = "";

        public const string ParameterUserName = "user name";

        public const string KickCommandName = "kick";
        public const string KickCommandAlias = "k";
        public const string KickCommandDescription = "Выгоняет {user name} с сервера.";

        public const string BanCommandName = "ban";
        public const string BanCommandAlias = "b";
        public const string BanCommandDescription = "Блокирует {user name} на сервере.";

        public const string MuteCommandName = "mute";
        public const string MuteCommandAlias = "m";
        public const string MuteCommandDescription = "Лишает всех прав {user name} на сервере.";

        public const string UnmuteCommandName = "unmute";
        public const string UnmuteCommandAlias = "um";
        public const string UnmuteCommandDescription = "Восстанавливает права {user name} на сервере.";
    }
}