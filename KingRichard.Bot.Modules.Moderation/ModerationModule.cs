using Discord.Commands;
using KingRichard.Bot.Modules.Attributes;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KingRichard.Bot.Modules.Moderation
{
    [Module(Constants.ModerationModuleName)]
    [Description(Constants.ModerationModuleDescription)]
    public class ModerationModule : ExtendDefaultModule
    {
        [Command(Constants.KickCommandName), Description(Constants.KickCommandDescription), Role(BotRoles.Moderator)]
        [Alias(Constants.KickCommandAlias)]
        [Parameter(Constants.ParameterUserName, ParameterType.Required)]
        public async Task Kick(CommandEventArgs e)
        {
            var user = e.Server.FindUsers(e.GetArg(Constants.ParameterUserName)).FirstOrDefault();

            if (user == null)
            {
                await SendStyleMessage(e.Channel, Strings.UserNotExist);
                return;
            }
            
            await user.Kick();

            await SendStyleMessage(e.Channel, String.Format(Strings.UserKicked, user.Name));
        }

        [Command(Constants.BanCommandName), Description(Constants.BanCommandDescription), Role(BotRoles.Moderator)]
        [Alias(Constants.BanCommandAlias)]
        [Parameter(Constants.ParameterUserName, ParameterType.Required)]
        public async Task Ban(CommandEventArgs e)
        {
            var user = e.Server.FindUsers(e.GetArg(Constants.ParameterUserName)).FirstOrDefault();

            if (user == null)
            {
                await SendStyleMessage(e.Channel, Strings.UserNotExist);
                return;
            }
       
            await e.Server.Ban(user);

            await SendStyleMessage(e.Channel, String.Format(Strings.UserBanned, user.Name));
        }

        [Command(Constants.MuteCommandName), Description(Constants.MuteCommandDescription), Role(BotRoles.Moderator)]
        [Alias(Constants.MuteCommandAlias)]
        [Parameter(Constants.ParameterUserName, ParameterType.Required)]
        public async Task Mute(CommandEventArgs e)
        {
            var user = e.Server.FindUsers(e.GetArg(Constants.ParameterUserName)).FirstOrDefault();

            if (user == null)
            {
                await SendStyleMessage(e.Channel, Strings.UserNotExist);
                return;
            }

            if (!e.Server.FindRoles(Constants.MuteRoleName).Any())
            {
                var role = await e.Server.CreateRole(Constants.MuteRoleName, Discord.ServerPermissions.None, Discord.Color.DarkGrey);

                foreach (var channel in e.Server.TextChannels)
                    await channel.AddPermissionsRule(role, Discord.ChannelPermissions.None, Discord.ChannelPermissions.All(channel));
            }
          
            await user.RemoveRoles(user.Roles.ToArray());
            await user.AddRoles(e.Server.FindRoles(Constants.MuteRoleName).First());

            await SendStyleMessage(e.Channel, String.Format(Strings.UserMuted, user.Name));
        }

        [Command(Constants.UnmuteCommandName), Description(Constants.UnmuteCommandDescription), Role(BotRoles.Moderator)]
        [Alias(Constants.UnmuteCommandAlias)]
        [Parameter(Constants.ParameterUserName, ParameterType.Required)]
        public async Task Unmute(CommandEventArgs e)
        {
            var user = e.Server.FindUsers(e.GetArg(Constants.ParameterUserName)).FirstOrDefault();

            if (user == null)
            {
                await SendStyleMessage(e.Channel, Strings.UserNotExist);
                return;
            }

            if (!e.Server.FindRoles(Constants.MuteRoleName).Any())
                await e.Server.CreateRole(Constants.MuteRoleName, Discord.ServerPermissions.None, Discord.Color.DarkGrey);

            var muteRole = e.Server.FindRoles(Constants.MuteRoleName).FirstOrDefault();

            if (muteRole == null)
                return;

            if (user.HasRole(muteRole))
                await user.RemoveRoles(muteRole);

            await SendStyleMessage(e.Channel, String.Format(Strings.UserUnmuted, user.Name));
        }
    }
}
