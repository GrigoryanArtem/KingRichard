using Discord;
using Discord.Commands;
using KingRichard.Bot.Modules.Attributes;
using KingRichard.Bot.Modules.Events;
using KingRichard.Data;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KingRichard.Bot.Modules.WordFilter
{
    [Module(Constants.FilterModuleName)]
    [Description(Constants.FilterModuleDescription)]
    public class FilterModule : ExtendDefaultModule
    {
        public FilterModule()
        {
            AddComponent(new BadSpeechFilter());
        }

        #region Events

        [BotEvent(Constants.RegexEventName, BotEvents.MessageUpdated)]
        public async void RegexUpdatedEvent(object sender, MessageUpdatedEventArgs args)
        {
            try
            {
                await RemoveMessageByFilter(sender, args);
            }
            catch { }
        }

        [BotEvent(Constants.RegexEventName, BotEvents.MessageReceived)]
        public async void RegexReceivedEvent(object sender, MessageEventArgs args)
        {
            try
            {
                await RemoveMessageByFilter(sender, args);
            }
            catch { }
        }

        #endregion

        #region Commands

        [Command(Constants.FilterListCommandName), Description(Constants.FilterListCommandDescription)]
        [Alias(Constants.FilterListCommandAlias)]
        public async Task FilterList(CommandEventArgs e)
        {
            await SendStyleMessage(e.Channel, FilterList((long)e.Server.Id));
        }

        [Command(Constants.AddFilterCommandName), Description(Constants.AddFilterCommandDescription), Role(BotRoles.Moderator)]
        [Alias(Constants.AddFilterCommandAlias)]
        [Parameter(Constants.Regex)]
        public async Task AddFilter(CommandEventArgs e)
        {
            string pattern = e.GetArg(Constants.Regex);
            long guildId = (long)e.Server.Id;

            if (IsValidRegex(pattern))
            {
                FilterService.Instance.Repository.Add(new Filter { GuildId = guildId, Regex = pattern });
                await SendStyleMessage(e.Channel, String.Format(Strings.FilterAdded, pattern));
            }
            else
            {
                await SendStyleMessage(e.Channel, String.Format(Strings.IsNotValidRegex, pattern));
            }
        }

        [Command(Constants.RemoveFilterCommandName), Description(Constants.RemoveFilterCommandDescription), Role(BotRoles.Moderator)]
        [Alias(Constants.RemoveFilterCommandAlias)]
        [Parameter(Constants.Id)]
        public async Task RemoveFilter(CommandEventArgs e)
        {
            long id = Convert.ToInt64(e.GetArg(Constants.Id));
            long guildId = (long)e.Server.Id;

            FilterService.Instance.Repository.RemoveFilterByGuildId(id, guildId);

            await SendStyleMessage(e.Channel, Strings.FilterRemoved);
        }

        #endregion

        #region Private methods

        private async Task RemoveMessageByFilter(object sender, MessageEventArgs args)
        {
            if (CheckEvent(args.Server))
                return;

            if (!args.User.IsBot && CheckMessege(args.Message.Text, (long)args.Server.Id))
            {
                await args.Message.Delete();
                var botAnswer = await args.Channel.SendMessage(Strings.RemoveMessege);

                await Task.Delay(Constants.Delay);
                await botAnswer.Delete();
            }
        }

        private async Task RemoveMessageByFilter(object sender, MessageUpdatedEventArgs args)
        {
            if (CheckEvent(args.Server))
                return;

            if (!args.User.IsBot && CheckMessege(args.After.Text, (long)args.Server.Id))
            {
                await args.After.Delete();
                var botAnswer = await args.Channel.SendMessage(Strings.RemoveMessege);

                await Task.Delay(Constants.Delay);
                await botAnswer.Delete();
            }
        }

        private string FilterList(long guildId)
        {
            StringBuilder stringBuilder = new StringBuilder();
            var filters = FilterService.Instance.Repository.GetFiltersByGuildId(guildId);

            foreach (var filter in filters)
                stringBuilder.AppendLine($"{filter.Id}. {filter.Regex}");

            return stringBuilder.ToString();
        }

        private bool CheckMessege(string messege, long guildId)
        {
            var patterns = FilterService.Instance.Repository.GetFiltersByGuildId(guildId);
            bool result = false;

            foreach (var pattern in patterns)
            {
                Regex regex = new Regex(pattern.Regex, RegexOptions.IgnoreCase);

                result = regex.IsMatch(messege);

                if (result)
                    break;
            }

            return result;
        }

        private static bool IsValidRegex(string pattern)
        {
            if (string.IsNullOrEmpty(pattern)) return false;

            try
            {
                Regex.Match("", pattern);
            }
            catch (ArgumentException)
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}
