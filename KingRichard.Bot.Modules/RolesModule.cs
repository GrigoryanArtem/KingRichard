using Discord.Commands;
using KingRichard.Bot.Modules.Attributes;
using KingRichard.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingRichard.Bot.Modules
{
    [Module(CommandsConstants.RolesModuleName)]
    [Description(CommandsConstants.RolesModuleDescription)]
    public class RolesModule : ExtendDefaultModule
    {
        [Permission(CommandsConstants.RolePermissionMessage)]
        public bool CheckRole(CommandEventArgs args, ICommand command)
        {
            return GetRealRole(args.User.Roles, (long)args.Server.Id) >= command.Role;
        }

        #region Commands

        [Command(CommandsConstants.RoleListCommandName), Description(CommandsConstants.RoleListCommandDescription)]
        [Alias(CommandsConstants.RoleListCommandAlias)]
        public async Task RoleList(CommandEventArgs e)
        {
            await SendStyleMessage(e.Channel, CreateRoleTable(e.Server.Id), SettingsModuleResource.Markdown);
        }

        [Command(CommandsConstants.ConnectRoleCommandName),
            Description(CommandsConstants.ConnectRoleCommandDescription) Role(BotRoles.Moderator)]
        [Alias(CommandsConstants.ConnectRoleCommandAlias)]
        [Parameter(CommandsConstants.ParameterDiscordRole)]
        [Parameter(CommandsConstants.ParameterRealRole)]
        public async Task AddRole(CommandEventArgs e)
        {
            string discordRole = e.GetArg(CommandsConstants.ParameterDiscordRole);
            string realRole = e.GetArg(CommandsConstants.ParameterRealRole);

            try
            {
                CheckRole(e.Server.Roles, discordRole);
                BotRoles botRole = ParseRole(realRole);

                RoleManager.RemoveRole(discordRole, (long)e.Server.Id);
                RoleManager.AddRole(discordRole, botRole, (long)e.Server.Id);

                await SendStyleMessage(e.Channel, SettingsModuleResource.RolesConnected, SettingsModuleResource.Markdown);
            }
            catch (ArgumentException exp)
            {
                await SendStyleMessage(e.Channel,
                    String.Format(SettingsModuleResource.RoleIsNotExist, exp.Message),
                    SettingsModuleResource.Markdown);
            }
        }

        [Command(CommandsConstants.DisonnectRoleCommandName),
            Description(CommandsConstants.DisonnectRoleCommandDescription), Role(BotRoles.Moderator)]
        [Alias(CommandsConstants.DisonnectRoleAlias)]
        [Parameter(CommandsConstants.ParameterDiscordRole)]
        public async Task RemoveRole(CommandEventArgs e)
        {
            string message = RoleManager.RemoveRole(e.GetArg(CommandsConstants.ParameterDiscordRole), (long)e.Server.Id) ?
                SettingsModuleResource.RoleDisconnected : SettingsModuleResource.RoleCantDsiconnect;

            await SendStyleMessage(e.Channel, message, SettingsModuleResource.Markdown);
        }

        #endregion

        #region Private methods

        private BotRoles GetRealRole(IEnumerable<Discord.Role> roles, long guildId)
        {
            BotRoles userRole = BotRoles.User;

            foreach (var role in roles)
            {
                BotRoles realRole = RoleManager.GetRealRole<BotRoles>(role.Name, guildId);

                if (realRole > userRole)
                    userRole = realRole;
            }

            return userRole;
        }

        private string CreateRoleTable(ulong guildId)
        {
            var roles = RoleManager.GetRoleList().
                Where(role => role.GuildId == (long)guildId);
            StringBuilder stringBuilder = new StringBuilder();

            foreach (var role in roles)
            {
                string roleName = role.Role.Length > CommandsConstants.NameLength ?
                    role.Role.Substring(0, CommandsConstants.NameLength - CommandsConstants.Ellipsis.Length) +
                    CommandsConstants.Ellipsis : role.Role;

                stringBuilder.AppendLine($"{roleName,-CommandsConstants.NameLength} | {role.RealRole}");
            }

            return stringBuilder.ToString();
        }

        private bool CheckRole(IEnumerable<Discord.Role> discorRoles, string role)
        {
            if (!discorRoles
                .Where(discordRole => discordRole.Name.Equals(role, StringComparison.OrdinalIgnoreCase))
                .Any())
                throw new ArgumentException(role);

            return true;
        }

        private BotRoles ParseRole(string role)
        {
            BotRoles botRole;

            if (!Enum.TryParse<BotRoles>(role, true, out botRole))
                throw new ArgumentException(role);

            return botRole;
        }

        #endregion
    }
}
