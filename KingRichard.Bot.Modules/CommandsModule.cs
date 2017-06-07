using KingRichard.Data;
using Discord.Commands;
using System;
using System.Text;
using System.Threading.Tasks;
using KingRichard.Bot.Modules.Commands;
using KingRichard.Bot.Modules.Attributes;
using System.Linq;

namespace KingRichard.Bot.Modules
{
    [Module(CommandsConstants.CommandsModuleName)]
    [Description(CommandsConstants.CommandsModuleDescription)]
    public class CommandsModule : ExtendDefaultModule
    {
        private IModulesObserver mBotModulesObserver;

        public CommandsModule(IModulesObserver botModulesObserver)
        {
            mBotModulesObserver = botModulesObserver;
        }

        [Permission(CommandsConstants.CommandPermissionMessage)]
        public bool CheckCommand(CommandEventArgs args, ICommand command)
        {
            return !command.IsBlocked((long)args.Server.Id);
        }

        #region Commands

        [Command(CommandsConstants.ModuleListCommandName), Description(CommandsConstants.ModuleListCommandDescription)]
        [Alias(CommandsConstants.ModuleListCommandAlias)]
        [Parameter(CommandsConstants.ParameterModuleName, ParameterType.Optional)]
        public async Task ModulesList(CommandEventArgs e)
        {
            await SendViewMessage(e, ModulePredicate.All);
        }

        [Command(CommandsConstants.ModuleListActiveCommandName), Description(CommandsConstants.ModuleListActiveCommandDescription)]
        [Alias(CommandsConstants.ModuleListActiveCommandAlias)]
        [Parameter(CommandsConstants.ParameterModuleName, ParameterType.Optional)]
        public async Task ActiveModulesList(CommandEventArgs e)
        {
            await SendViewMessage(e, ModulePredicate.Unblocked);
        }

        [Command(CommandsConstants.ModuleListInactiveCommandName), Description(CommandsConstants.ModuleListInactiveCommandDescription)]
        [Alias(CommandsConstants.ModuleListInactiveCommandAlias)]
        [Parameter(CommandsConstants.ParameterModuleName, ParameterType.Optional)]
        public async Task InactiveModulesList(CommandEventArgs e)
        {
            await SendViewMessage(e, ModulePredicate.Blocked);
        }

        [Command(CommandsConstants.ActivateModuleCommandName), Description(CommandsConstants.ActivateModuleCommandDescription), Role(BotRoles.Admin)]
        [Alias(CommandsConstants.ActivateModuleCommandAlias)]
        [Parameter(CommandsConstants.ParameterModuleName)]
        public async Task ActivateModule(CommandEventArgs e)
        {

            try
            {
                IModule module = CommandsManager.FindModule(e.GetArg(CommandsConstants.ParameterModuleName), mBotModulesObserver.Modules);

                if (!module.IsBlocked((long)e.Server.Id))
                {
                    await SendStyleMessage(e.Channel, SettingsModuleResource.ModuleAlreadyUnblocked, SettingsModuleResource.Markdown);
                }
                else
                {
                    var blockedModuleId = BlockedModuleService.Instance.Repository.GetByName(module.Name, (long)e.Server.Id).Id;
                    BlockedModuleService.Instance.Repository.RemoveById(blockedModuleId);

                    await SendStyleMessage(e.Channel, String.Format(SettingsModuleResource.ModuleUnblocked, module.Name), SettingsModuleResource.Markdown);
                }
            }
            catch (ArgumentException exp)
            {
                await SendStyleMessage(e.Channel, exp.Message, SettingsModuleResource.Markdown);
            }
        }

        [Command(CommandsConstants.DeactivateModuleCommandName), Description(CommandsConstants.DeactivateModuleCommandDescription), Role(BotRoles.Admin)]
        [Alias(CommandsConstants.DeactivateModuleCommandAlias)]
        [Parameter(CommandsConstants.ParameterModuleName)]
        public async Task DeactivateModule(CommandEventArgs e)
        {
            try
            {
                IModule module = CommandsManager.FindModule(e.GetArg(CommandsConstants.ParameterModuleName), mBotModulesObserver.Modules);

                if (module.IsBlocked((long)e.Server.Id))
                {
                    await SendStyleMessage(e.Channel, SettingsModuleResource.ModuleAlreadyBlocked, SettingsModuleResource.Markdown);
                }
                else
                {
                    BlockedModuleService.Instance.Repository.Add(new BlockedModule { ModuleName = module.Name, GuildId = (long)e.Server.Id });
                    await SendStyleMessage(e.Channel, String.Format(SettingsModuleResource.ModuleBlocked, module.Name), SettingsModuleResource.Markdown);
                }
            }
            catch (ArgumentException exp)
            {
                await SendStyleMessage(e.Channel, exp.Message, SettingsModuleResource.Markdown);
            }
        }

        [Command(CommandsConstants.ActivateCommandCommandName), Description(CommandsConstants.ActivateCommandCommandDescription), Role(BotRoles.Admin)]
        [Alias(CommandsConstants.ActivateCommandCommandAlias)]
        [Parameter(CommandsConstants.ParameterCommandName)]
        public async Task ActivateCommand(CommandEventArgs e)
        {

            try
            {
                ICommand command = CommandsManager.FindCommand(e.GetArg(CommandsConstants.ParameterCommandName), mBotModulesObserver.Modules);

                if (!command.IsBlocked((long)e.Server.Id))
                {
                    await SendStyleMessage(e.Channel, SettingsModuleResource.CommandAlreadyUnblocked, SettingsModuleResource.Markdown);
                }
                else
                {
                    var blockedCommandId = BlockedCommandService.Instance.Repository.GetByName(command.Name, (long)e.Server.Id).Id;
                    BlockedCommandService.Instance.Repository.RemoveById(blockedCommandId);

                    await SendStyleMessage(e.Channel, String.Format(SettingsModuleResource.CommandUnblocked, command.Name), SettingsModuleResource.Markdown);
                }
            }
            catch (ArgumentException exp)
            {
                await SendStyleMessage(e.Channel, exp.Message, SettingsModuleResource.Markdown);
            }
        }

        [Command(CommandsConstants.DeactivateCommandCommandName), Description(CommandsConstants.DeactivateCommandCommandDescription), Role(BotRoles.Admin)]
        [Alias(CommandsConstants.DeactivateCommandCommandAlias)]
        [Parameter(CommandsConstants.ParameterCommandName)]
        public async Task DeactivateCommand(CommandEventArgs e)
        {
            try
            {
                ICommand command = CommandsManager.FindCommand(e.GetArg(CommandsConstants.ParameterCommandName), mBotModulesObserver.Modules);

                if (command.IsBlocked((long)e.Server.Id))
                {
                    await SendStyleMessage(e.Channel, SettingsModuleResource.CommandAlreadyBlocked, SettingsModuleResource.Markdown);
                }
                else
                {
                    BlockedCommandService.Instance.Repository.Add(new BlockedCommand{ CommandName=command.Name, GuildId = (long)e.Server.Id });
                    await SendStyleMessage(e.Channel, String.Format(SettingsModuleResource.CommandBlocked, command.Name), SettingsModuleResource.Markdown);
                }
            }
            catch(ArgumentException exp)
            {
                await SendStyleMessage(e.Channel, exp.Message, SettingsModuleResource.Markdown);
            }
        }

        #endregion

        #region Private methods

        private async Task SendViewMessage(CommandEventArgs e, Func<IComponent, long, bool> predicate)
        {
            try
            {
                CommandsManager manager = new CommandsManager((long)e.Server.Id, predicate);

                string name = e.GetArg(CommandsConstants.ParameterModuleName);
                string view = String.IsNullOrEmpty(name) ? manager.CreateComponentsView(mBotModulesObserver.Modules) :
                    manager.CreateComponentsView(CommandsManager.FindModule(name, mBotModulesObserver.Modules));

                await SendStyleMessage(e.Channel, view, SettingsModuleResource.Css);
            }
            catch(ArgumentException exp)
            {
                await SendExceptonMessage(e.Channel, exp);
            }
        }

        private async Task SendExceptonMessage(Discord.Channel channel, ArgumentException exp)
        {
            await SendStyleMessage(channel, exp.Message, SettingsModuleResource.Css);
        }

        #endregion
    }
}
