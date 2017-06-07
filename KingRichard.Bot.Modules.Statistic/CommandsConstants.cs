namespace KingRichard.Bot.Modules.Statistic
{
    internal class CommandsConstants
    {
        public const string AddUsersEventName = "add users handler";
        public const string AddChannelsEventName = "add channels handler";
        public const string UserLeftEventName = "user left handler";
        public const string UserJoinEventName = "user left handler";
        public const string UserJoinVoiceChannelEventName = "user join voice channel handler";
        public const string UserJoinApplicationEventName = "user join application handler";
        public const string AddChannelEventName = "add channel handler";
        public const string RemoveChannelEventName = "remove channel handler"; 
        public const string StatisticModuleName = "Statistic";
        public const string StatisticModuleDescription = "";

        #region Stats

        public const string StatsCommandAlias = "st";
        public const string StatsCommandName = "stats";
        public const string StatsDescription = "Выводит статистику пользователя.";

        #endregion
    }
}
