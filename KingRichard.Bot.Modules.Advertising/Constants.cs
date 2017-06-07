namespace KingRichard.Bot.Modules.Advertising
{
    internal class Constants
    {
        public const string AdvertisingModuleName = "Advertising";
        public const string AdvertisingModuleDescription = "";

        public const string AddGuildEventName = "add guilds handler";
        public const string RemoveGuildEventName = "remove guild handler";
        static internal int StartTimerTime
        {
            get
            {
                return 30000;
            }
        }

        static internal int TimerTick
        {
            get
            {
                return 3600000;
            }
        }

        static internal int Delay
        {
            get
            {
                return 300000;
            }
        }
    }
}