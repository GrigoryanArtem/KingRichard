using Discord;
using Discord.Commands;
using System;
using KingRichard.Bot.Modules;

namespace KingRichard.Bot
{
    public class DiscordBot
    {
        #region Members

        private CommandService commandService;
        private IModulesController mController;

        #endregion

        #region Properties

        public string BotToken { get; set; }

        public char PrefixChar { get; set; } = BotConstants.DefaultPrefixChar;

        public IModulesObserver Modules
        {
            get
            {
                return mController;
            }
        }

        #endregion

        #region Constructors

        public DiscordBot(EventHandler<LogMessageEventArgs> func, IModulesController modulesController)
        {
            var client = new DiscordClient(input =>
            {
                input.LogLevel = LogSeverity.Info;
                input.LogHandler = func;
            });

            client.UsingCommands(input =>
            {
                input.PrefixChar = PrefixChar;
                input.AllowMentionPrefix = true;
                input.HelpMode = HelpMode.Private;
            });

            commandService = client.GetService<CommandService>();

            mController = modulesController;
            mController.Init(commandService);
        }

        #endregion

        #region Public methods

        public void Start()
        {
            mController.Run();

            commandService.Client.ExecuteAndWait(async () =>
            {
                await commandService.Client.Connect(BotToken, TokenType.Bot);
            });
        }

        #endregion
    }
}
