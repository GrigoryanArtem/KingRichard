using Discord.Commands;
using System.Reflection;
using System.Threading.Tasks;
using KingRichard.Data;
using System;
using KingRichard.Bot.Modules.Attributes;

namespace KingRichard.Bot.Modules
{
    public abstract class BotCommand : ICommand
    {
        protected MethodInfo mMethod;

        #region Properties

        public string Name { get; protected set; }

        public IModule ParentModule { get; set; }

        public BotRoles Role { get; protected set; } = BotConstants.DeafultUserRole;

        #endregion

        public bool IsBlocked(long guildId)
        {
            return !(BlockedCommandService.Instance.Repository.GetByName(Name, guildId) == null) 
                || (ParentModule != BotConstants.EmptyParent ? ParentModule.IsBlocked(guildId) : false);
        }

        public BotCommand(IModule parentModule, MethodInfo commandMethod)
        {
            ParentModule = parentModule;
            mMethod = commandMethod;

            Init();
        }

        public void Run(CommandService service)
        {
            var command = service.CreateCommand(Name);

            SetAliases(command);
            SetDescription(command);
            SetParameters(command);

            command.Do(async (args) =>
            {
                try
                {
                    if (args.Server == null)
                        return;

                    await args.Message.Delete();
                    InitCommand(args);

                    Task function = (Task)mMethod.Invoke(ParentModule, new object[] { args });         
                    await function;
                }
                catch (BotException exp)
                {
                    var errorMessege = await args.Channel.SendMessage(String.Format(Resources.ErrorMessage, exp.Message));

                    await Task.Delay(BotConstants.Delay);
                    await errorMessege.Delete();
                }
            });
        }

        protected abstract void InitCommand(CommandEventArgs args);

        #region Private methods

        private void Init()
        {
            CommandAttribute commandAttribute = mMethod.GetCustomAttribute<CommandAttribute>();        
            Name = commandAttribute.CommandName;

            RoleAttribute roleAttribute = mMethod.GetCustomAttribute<RoleAttribute>();

            if(roleAttribute != null)
                Role = roleAttribute.AvailableRole;
        }

        private void SetAliases(CommandBuilder command)
        {
            var aliases = mMethod.GetCustomAttribute<AliasAttribute>();

            if (aliases != null)
                command.Alias(aliases.Aliases);
        }

        private void SetDescription(CommandBuilder command)
        {
            var description = mMethod.GetCustomAttribute<DescriptionAttribute>();

            if (description != null)
                command.Description(description.DescriptionText);
        }

        private void SetParameters(CommandBuilder command)
        {
            var parameters = mMethod.GetCustomAttributes<ParameterAttribute>();

            foreach (var parameter in parameters)
                command.Parameter(parameter.ParameterName, parameter.ParameterType);
        } 

        #endregion
    }
}
