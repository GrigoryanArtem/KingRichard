using System.Threading.Tasks;
using System.Collections.Generic;
using Discord;
using Discord.Commands;
using System;
using KingRichard.Data;
using KingRichard.Bot.Modules.Attributes;

namespace KingRichard.Bot.Modules
{
    public abstract class DefaultCommandModule : IModule
    {
        #region Members

        protected List<IComponent> mComponents = new List<IComponent>();

        #endregion

        public DefaultCommandModule(IModule parent)
        {
            Init();
            ParentModule = parent;
        }

        public DefaultCommandModule() : this(BotConstants.EmptyParent) { }

        public IComponent[] Components
        {
            get
            {
                return mComponents.ToArray();
            }
        }

        public IModule ParentModule { get; set; }

        public string Description { get; protected set; }

        public string Name { get; protected set; }

        public virtual void Run(CommandService commandService)
        {
            foreach (var command in mComponents)
                command.Run(commandService);
        }

        public bool IsBlocked(long guildId)
        {
            return !(BlockedModuleService.Instance.Repository.GetByName(Name, guildId) == null)
                || (ParentModule != BotConstants.EmptyParent ? ParentModule.IsBlocked(guildId) : false);
        }

        #region Protected methods

        protected void AddComponent(IComponent component)
        {
            component.ParentModule = this;
            mComponents.Add(component);
        }

        protected abstract void AddCommands();

        protected async Task SendStyleMessage(Channel channel, string message, string style = BotConstants.DefaultStyle)
        {         
            await channel.SendMessage(Format.Code(message, style));
        }

        #endregion

        private void Init()
        {
            var thisClass = this.GetType();

            var module = Attribute.GetCustomAttribute(thisClass, typeof(ModuleAttribute)) as ModuleAttribute;  
            Name = module.Name;

            var description = Attribute.GetCustomAttribute(thisClass, typeof(DescriptionAttribute)) as DescriptionAttribute;
            Description = description != null ? description.DescriptionText : String.Empty;
        }
    }
}
