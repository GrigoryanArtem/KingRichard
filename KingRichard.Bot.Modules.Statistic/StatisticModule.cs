using Discord;
using Discord.Commands;
using KingRichard.Bot.Modules.Attributes;
using KingRichard.Bot.Modules.Events;
using KingRichard.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingRichard.Bot.Modules.Statistic
{
    [Module(CommandsConstants.StatisticModuleName)]
    [Description(CommandsConstants.StatisticModuleDescription)]
    public class StatisticModule : ExtendDefaultModule
    {
        private Dictionary<long, DateTime> startedSessions = new Dictionary<long, DateTime>();

        #region Commands

        [Command(CommandsConstants.StatsCommandName), Description(CommandsConstants.StatsDescription)]
        [Alias(CommandsConstants.StatsCommandAlias)]
        public async Task Stats(CommandEventArgs e)
        {
            var user = UserService.Instance.Repository.GetById((long)e.User.Id);
            await SendStyleMessage(e.User.PrivateChannel, GenerateStatistic(user));
        }

        #endregion

        #region Events

        [BotEvent(CommandsConstants.AddUsersEventName, BotEvents.ServerAvailable)]
        public void AddUsers(object sender, ServerEventArgs args)
        {
            if (CheckEvent(args.Server))
                return;

            if (GuildService.Instance.Repository.GetById((long)args.Server.Id) == null)
                GuildService.Instance.Repository.Add(new Guild { Id = (long)args.Server.Id, Name = args.Server.Name});

            var users = args.Server.Users.Where(user => !user.IsBot);
            foreach (var user in users)
            {
                long userId = (long)user.Id;

                if (UserService.Instance.Repository.GetById(userId) == null)
                    UserService.Instance.Repository.Add(new Data.User { Id = userId, Name = user.Name, Type = UserType.Default });

                if (!startedSessions.ContainsKey(userId) && user.Status == UserStatus.Online)
                    startedSessions.Add(userId, DateTime.Now);
            }
        }

        [BotEvent(CommandsConstants.AddChannelsEventName, BotEvents.ServerAvailable)]
        public void AddChannels(object sender, ServerEventArgs args)
        {
            if (CheckEvent(args.Server))
                return;

            var channels = args.Server.VoiceChannels; 

            foreach(var channel in channels)
            {
                long channelId = (long)channel.Id;

                if (channel.Type == ChannelType.Voice)
                {
                    if (VoiceChannelService.Instance.Repository.GetById(channelId) == null)
                        VoiceChannelService.Instance.Repository.Add(new VoiceChannel
                        { Id = channelId, GuildId = (long)args.Server.Id, Name = channel.Name });
                }
            }
        }

        [BotEvent(CommandsConstants.RemoveChannelEventName, BotEvents.ChannelDestroyed)]
        public void RemoveChannel(object sender, ChannelEventArgs args)
        {
            if (CheckEvent(args.Server))
                return;

            VoiceChannelService.Instance.Repository.RemoveById((long)args.Channel.Id);
        }

        [BotEvent(CommandsConstants.AddChannelEventName, BotEvents.ChannelCreated)]
        public void AddChannel(object sender, ChannelEventArgs args)
        {
            if (CheckEvent(args.Server))
                return;

            var channel = args.Channel;

            if (channel.Type == ChannelType.Voice)
            {
                VoiceChannelService.Instance.Repository.Add(new VoiceChannel
                { Id = (long)channel.Id, GuildId = (long)args.Server.Id, Name = channel.Name });
            }
        }

        [BotEvent(CommandsConstants.UserJoinEventName, BotEvents.UserUpdated)]
        public void UserJoin(object sender, UserUpdatedEventArgs args)
        {
            if (CheckEvent(args.Server))
                return;

            long userId = (long)args.Before.Id;

            if ((args.After.Status == UserStatus.Offline) && startedSessions.ContainsKey(userId))
            {
                SessionService.Instance.Repository.Add(new Session { StartTime = startedSessions[userId], EndTime = DateTime.Now, UserId = userId });
                startedSessions.Remove(userId);
            }
        }

        [BotEvent(CommandsConstants.UserLeftEventName, BotEvents.UserUpdated)]
        public void UserLeft(object sender, UserUpdatedEventArgs args)
        {
            if (CheckEvent(args.Server))
                return;

            long userId = (long)args.Before.Id;
            
            if ((args.After.Status == UserStatus.Online) && !startedSessions.ContainsKey(userId))
                startedSessions.Add(userId, DateTime.Now);
        }


        [BotEvent(CommandsConstants.UserJoinVoiceChannelEventName, BotEvents.UserUpdated)]
        public void UserJoinVoiceChannael(object sender, UserUpdatedEventArgs args)
        {
            if (CheckEvent(args.Server))
                return;

            if (!args.After.IsBot && args.After.VoiceChannel != null && args.Before.VoiceChannel != args.After.VoiceChannel)
                VoiceChannelLogService.Instance.Repository.Add(new VoiceChannelLog
                { Time = DateTime.Now, UserId = (long)args.After.Id, VoiceChannelId = (long)args.After.VoiceChannel.Id });
        }

        [BotEvent(CommandsConstants.UserJoinApplicationEventName, BotEvents.UserUpdated)]
        public void UserJoinApp(object sender, UserUpdatedEventArgs args)
        {
            if (CheckEvent(args.Server))
                return;

            if (!args.After.IsBot && args.Before.CurrentGame == null && args.After.CurrentGame != null)
            {
                if(!ApplicationLogService.Instance.Repository
                    .Get(app => app.ApplicationName == args.After.CurrentGame.Value.Name 
                    && ((DateTime.Now - app.Time).TotalSeconds < 10)).Any())
                    ApplicationLogService.Instance.Repository.Add(new ApplicationLog
                    { Time = DateTime.Now, UserId = (long)args.After.Id, ApplicationName = args.After.CurrentGame.Value.Name });
            }
        }

        #endregion

        #region Private methods

        private string GenerateStatistic(Data.User user)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(String.Format(Strings.AccountTypeFormat, user.Type.ToString()));
            stringBuilder.AppendLine(Strings.Sessions);

            var sessions = user.Sessions.OrderByDescending(s => s.EndTime).Take(5);
            foreach (var session in sessions)
                stringBuilder.AppendLine($"    | {GetDate(session.StartTime)} - {GetDate(session.EndTime)} | {GetTime(session.EndTime - session.StartTime)} |");

            stringBuilder.AppendLine(Strings.VoiceChannels);

            var channels = VoiceChannelLogService.Instance.Repository.GetByUserId(user.Id)
                .GroupBy(channel => channel.VoiceChannelId)
                .OrderByDescending(t => t.Key)
                .Take(5)
                .Select(group => group.First());
                
            foreach (var channel in channels)
                stringBuilder.AppendLine($"    {channel.VoiceChannel.Name} ({channel.VoiceChannel.Guild.Name})");

            stringBuilder.AppendLine(Strings.Applications);

            var apps = ApplicationLogService.Instance.Repository.GetByUserId(user.Id)
                .GroupBy(app => app.ApplicationName)
                .OrderByDescending(t => t.Key)
                .Take(5)
                .Select(group => group.First());

            foreach (var app in apps)
                stringBuilder.AppendLine($"    {app.ApplicationName}");

            return stringBuilder.ToString();
        }

        private string GetDate(DateTime date)
        {
            return date.ToString(Strings.DateFormat, CultureInfo.InvariantCulture);
        }

        private string GetTime(TimeSpan time)
        {
            return String.Format("{0:D3}:{1:D2}:{2:D2}", (int)time.TotalHours, time.Minutes, time.Seconds);
        }

        #endregion
    }
}
