using Discord;
using Discord.Commands;
using KingRichard.Bot.Modules.Attributes;
using KingRichard.Bot.Modules.Events;
using KingRichard.Communication;
using KingRichard.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingRichard.Bot.Modules
{
    [Module(CommandsConstants.SettingsModuleName)]
    [Description(CommandsConstants.SettingsModuleDescription)]
    public class SettingsModule : ExtendDefaultModule
    {
        public SettingsModule(IModulesObserver botModulesObserver)
        {
            AddComponent(new CommandsModule(botModulesObserver));
            AddComponent(new RolesModule());
        }

        #region Commands

        [Command(CommandsConstants.TimeCommandName), Description(CommandsConstants.TimeCommandDescription)]
        [Alias(CommandsConstants.TimeCommandAlias)]
        public async Task Time(CommandEventArgs e)
        {
            await SendStyleMessage(e.Channel, CreateTimeMessage(), SettingsModuleResource.Markdown);
        }

        [Command(CommandsConstants.ClearCommandName), Description(CommandsConstants.ClearCommandDescription), Role(BotRoles.Moderator)]
        [Alias(CommandsConstants.ClearCommandAlias)]
        [Parameter(CommandsConstants.ParameterUserName, ParameterType.Required)]           
        public async Task Clear(CommandEventArgs e)
        {
            string userName = e.GetArg(CommandsConstants.ParameterUserName);
            int count = 0;

            await e.Channel.DownloadMessages();

            foreach (var message in e.Channel.Messages)
            {
                if (message.User != null && message.User.Name.Equals(userName, StringComparison.OrdinalIgnoreCase))
                {
                    await message.Delete();
                    count++;
                }
            }

            var removeMessage = await e.Channel.SendMessage(String.Format(SettingsModuleResource.RemoveMessageFormat, count, userName));

            await Task.Delay(CommandsConstants.Delay);
            await removeMessage.Delete();
        }


        [Command(CommandsConstants.SayCommandName), Role(BotRoles.Admin)]
        [Alias(CommandsConstants.SayCommandAlias)]
        [Parameter(CommandsConstants.ParameterText, ParameterType.Unparsed)]
        public async Task Say(CommandEventArgs e)
        {
            await e.Channel.SendTTSMessage(e.GetArg(CommandsConstants.ParameterText));
        }

        #endregion

        #region Events 

        [BotEvent(CommandsConstants.AddGuildEventName, BotEvents.JoinedServer)]
        public void AddGuild(object sender, ServerEventArgs args)
        {
            if (CheckEvent(args.Server))
                return;

            GuildService.Instance.Repository.Add(new Guild { Id = (long)args.Server.Id, Name = args.Server.Name });
        }

        [BotEvent(CommandsConstants.RemoveGuildEventName, BotEvents.LeftServer)]
        public void RemoveGuild(object sender, ServerEventArgs args)
        {
            if (CheckEvent(args.Server))
                return;

            GuildService.Instance.Repository.RemoveById((long)args.Server.Id);
        }

        #endregion

        #region Private methods

        private string CreateTimeMessage()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(DateTime.Now.ToLongTimeString());
            sb.AppendLine(DateTime.Now.ToLongDateString());

            return sb.ToString();
        }

        #endregion
    }
}