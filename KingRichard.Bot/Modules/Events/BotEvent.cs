using System;
using Discord.Commands;
using System.Reflection;
using KingRichard.Bot.Modules.Attributes;

namespace KingRichard.Bot.Modules.Events
{
    public class BotEvent : IComponent
    {
        private string mEvent;
        protected MethodInfo mMethod;

        public BotEvent(IModule parentModule, MethodInfo commandMethod)
        {
            ParentModule = parentModule;
            mMethod = commandMethod;

            Init();
        }

        public string Name { get; protected set; }

        public IModule ParentModule { get; set; }

        public bool IsBlocked(long guildId)
        {
            return ParentModule.IsBlocked(guildId);
        }

        public void Run(CommandService commandService)
        {
            var clientType = commandService.Client.GetType();
            var eventInfo = clientType.GetEvent(mEvent);

            Delegate handler = Delegate.CreateDelegate(eventInfo.EventHandlerType, ParentModule, mMethod);
            eventInfo.AddEventHandler(commandService.Client, handler);
        }

        private void Init()
        {
            BotEventAttribute commandAttribute = mMethod.GetCustomAttribute<BotEventAttribute>();

            Name = commandAttribute.EventName;
            mEvent = commandAttribute.Event;
        }
    }
}
