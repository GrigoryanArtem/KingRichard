using KingRichard.Bot;
using KingRichard.Bot.Modules;
using KingRichard.Bot.Modules.WordFilter;
using KingRichard.Bot.Modules.Statistic;
using KingRichard.Bot.Modules.Advertising;
using KingRichard.Bot.Modules.Moderation;

namespace KingRichard.CUI
{
    class Program
    {
        static void Main(string[] args)
        {        
            DiscordBot bot = LogBotService.CreateBot();

            bot.BotToken = Properties.Settings.Default.BotToken;

            bot.Modules.Add(new StatisticModule());
            bot.Modules.Add(new SettingsModule(bot.Modules));
            bot.Modules.Add(new MathModule());
            bot.Modules.Add(new FilterModule());
            bot.Modules.Add(new AdvertisingModule());
            bot.Modules.Add(new ModerationModule());

            bot.Start();
        }
    }
}
