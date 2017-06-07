using System;

namespace KingRichard.Bot.Modules.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class BotEventAttribute : Attribute
    {
        public string EventName { get; set; }
        public string Event { get; set; }

        public BotEventAttribute(string eventName, string @event)
        {
            EventName = eventName;
            Event = @event;          
        }
    }
}
