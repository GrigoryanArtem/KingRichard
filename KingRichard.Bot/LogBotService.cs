using System;
using Discord;
using KingRichard.Bot.Modules;

namespace KingRichard.Bot
{
    public static class LogBotService
    {
        public static DiscordBot CreateBot()
        {
            return new DiscordBot(Logger, new DefaultModulesController());
        }
        private static void Logger(object sender, LogMessageEventArgs e)
        {
            var cc = Console.ForegroundColor;

            switch (e.Severity)
            {
                case LogSeverity.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case LogSeverity.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case LogSeverity.Info:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case LogSeverity.Verbose:
                case LogSeverity.Debug:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
            }

            Console.WriteLine($"{DateTime.Now} [{e.Severity}] {e.Source}: {e.Message}");
            Console.ForegroundColor = cc;
        }
    }
}
