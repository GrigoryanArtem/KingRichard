namespace KingRichard.Bot.Modules
{
    internal class CommandsConstants
    {
        public const int Delay = 5000;
        public const int NameLength = 13;

        public const string Ellipsis = "...";

        #region Events

        public const string AddGuildEventName = "add server handler";
        public const string RemoveGuildEventName = "remove server handler";

        #endregion

        #region Permission messages

        public const string RolePermissionMessage = "Ваша роль не подходит";
        public const string CommandPermissionMessage = "Команда отключена";

        #endregion

        #region Parametrs 

        public const string ParameterUserName = "user name";
        public const string ParameterDiscordRole = "discord role";
        public const string ParameterRealRole = "real role";
        public const string ParameterCommandName = "command name";
        public const string ParameterText = "text";
        public const string ParameterModuleName = "module name";
        public const string Expression = "expression";
        #endregion

        #region Core module

        public const string SettingsModuleName = "Core";
        public const string SettingsModuleDescription = "";

        #endregion

        #region Roles module

        public const string RolesModuleName = "Roles";
        public const string RolesModuleDescription = "";

        #endregion

        #region Math module

        public const string MathModuleName = "Math";
        public const string MathModuleDescription = "";

        #endregion

        #region Commands module

        public const string CommandsModuleName = "Commands";
        public const string CommandsModuleDescription = "";

        #endregion

        #region Activate command

        public const string ActivateCommandCommandAlias = "ac";
        public const string ActivateCommandCommandName = "activate command";
        public const string ActivateCommandCommandDescription = "Включает комманду.";

        #endregion

        #region Deactivate command

        public const string DeactivateCommandCommandAlias = "dc";
        public const string DeactivateCommandCommandName = "deactivate command";
        public const string DeactivateCommandCommandDescription = "Отключает комманду.";

        #endregion

        #region Activate module

        public const string ActivateModuleCommandAlias = "am";
        public const string ActivateModuleCommandName = "activate module";
        public const string ActivateModuleCommandDescription = "Включает модуль.";

        #endregion

        #region Deactivate module

        public const string DeactivateModuleCommandAlias = "dm";
        public const string DeactivateModuleCommandName = "deactivate module";
        public const string DeactivateModuleCommandDescription = "Отключает модуль.";

        #endregion

        #region Module list

        public const string ModuleListCommandAlias = "ml";
        public const string ModuleListCommandName = "module list";
        public const string ModuleListCommandDescription = "Показывает текущий список модулей и комманд.";

        #endregion

        #region Module list

        public const string ModuleListActiveCommandAlias = "mla";
        public const string ModuleListActiveCommandName = "module list active";
        public const string ModuleListActiveCommandDescription = "Показывает текущий список активных модулей и комманд.";

        #endregion

        #region Module list

        public const string ModuleListInactiveCommandAlias = "mli";
        public const string ModuleListInactiveCommandName = "module list inactive";
        public const string ModuleListInactiveCommandDescription = "Показывает текущий список деактивированных модулей и комманд.";

        #endregion

        #region Say

        public const string SayCommandAlias = "s";
        public const string SayCommandName = "say";
        public const string SayCommandDescription = "Зачитывает <text>.";

        #endregion

        #region Calculate

        public const string CalculateCommandAlias = "calc";
        public const string CalculateCommandName = "calculate";
        public const string CalculateCommandDescription = "Вычисляет результат математического выражения.";

        #endregion

        #region Time

        public const string TimeCommandName = "time";
        public const string TimeCommandDescription = "Печатает текущую дату и время.";
        public const string TimeCommandAlias = "t";

        #endregion

        #region Clear

        public const string ClearCommandAlias = "cls";
        public const string ClearCommandName = "clear";
        public const string ClearCommandDescription = "Очищает сообщение пользователя.";

        #endregion

        #region Role list

        public const string RoleListCommandAlias = "rl";
        public const string RoleListCommandName = "role list";
        public const string RoleListCommandDescription = "Показывает текущий список ролей.";

        #endregion

        #region Connect Role

        public const string ConnectRoleCommandAlias = "cr";
        public const string ConnectRoleCommandName = "connect role";
        public const string ConnectRoleCommandDescription = "Связывает роли.";

        #endregion

        #region Disonnect Role

        public const string DisonnectRoleAlias = "dr";
        public const string DisonnectRoleCommandName = "disconnect role";
        public const string DisonnectRoleCommandDescription = "Разъединяет роли.";

        #endregion
    }
}