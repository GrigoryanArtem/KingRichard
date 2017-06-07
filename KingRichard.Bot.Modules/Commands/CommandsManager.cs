using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KingRichard.Bot.Modules.Commands
{
    public class CommandsManager
    {
        private long mGuildId;
        private Func<IComponent, long, bool> mPredicate;

        public CommandsManager(long guildId) : this(guildId, ModulePredicate.All) { }

        public CommandsManager(long guildId, Func<IComponent, long, bool> predicate)
        {
            mGuildId = guildId;
            mPredicate = predicate;
        }

        #region Public methods

        public string CreateComponentsView(params IComponent[] components)
        {
            return CreateComponentsView(components, CommandsManagerConstants.TreeLevel);
        }

        public static IModule FindModule(string name, params IComponent[] components)
        {
            var result = FindModuleByName(name, components);

            if (result == null)
                throw new ArgumentException(SettingsModuleResource.FindModuleException);

            return (result as IModule);
        }

        public static ICommand FindCommand(string name, params IComponent[] components)
        {
            var result = FindCommandByName(name, components);

            if(result == null)
                throw new ArgumentException(SettingsModuleResource.FindCommandException);

            return (result as ICommand);
        }

        #endregion

        #region private methods

        private static IComponent FindCommandByName(string name, IComponent[] components)
        {
            ICommand result = null;

            IEnumerable<ICommand> commands = components
                .Where(component => component is ICommand)
                .Select(component => (component as ICommand));

            foreach (var command in commands)
                if (command.Name == name)
                    return command;


            IEnumerable<IModule> modules = components
                .Where(component => component is IModule)
                .Select(component => (component as IModule));

            foreach (var module in modules)
            {
                result = (FindCommandByName(name, module.Components) as ICommand);

                if (result != null)
                    return result;
            }

            return null;
        }

        private static IComponent FindModuleByName(string name, IComponent[] components)
        {
            IModule result = null;
            IEnumerable<IModule> modules = components
                .Where(component => component is IModule)
                .Select(component => (component as IModule));

            foreach (var module in modules)
            {
                if (String.Equals(module.Name, name, StringComparison.OrdinalIgnoreCase))
                    return module;

                result = (FindModuleByName(name, module.Components) as IModule);

                if (result != null)
                    return result;
            }

            return null;
        }

        private string CreateComponentsView(IComponent[] components, int level)
        {
            StringBuilder result = new StringBuilder();

            foreach (var component in components)
                    result.Append(CreateComponentView(component, level));

            return result.ToString();
        }

        private string CreateComponentView(IComponent component, int level)
        {
            StringBuilder result = new StringBuilder();

            if (mPredicate(component, mGuildId))
                result.AppendLine(String.Format(CommandsManagerConstants.VertexFormat, CreateIndent(level),
                (component is IModule) ? CommandsManagerConstants.ModuleLabel : CommandsManagerConstants.CommandLabel,
                component.IsBlocked(mGuildId) ? CommandsManagerConstants.MinusLabel : CommandsManagerConstants.PlusLabel, component.Name));

            if (component is IModule)
                result.Append(CreateComponentsView((component as IModule).Components, level + CommandsManagerConstants.TreeDeviation));

            return result.ToString();
        }

        private static string CreateIndent(int level)
        {
            return new String(CommandsManagerConstants.TreeBranch, level);
        }

        #endregion
    }
}