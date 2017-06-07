using Discord.Commands;
using KingRichard.Bot.Modules.Attributes;
using KingRichard.Bot.Modules.Events;
using System.Collections.Generic;
using System.Linq;

namespace KingRichard.Bot.Modules
{
    public class ExtendDefaultModule : ModuleWithPermissions
    {
        List<BotEvent> events = new List<BotEvent>();

        public ExtendDefaultModule(IModule parent)
            : base(parent, PermissionControllerService.Instance.Create())
        {
            AddEvents();
        }

        public ExtendDefaultModule() : this(null) { }


        public override void Run(CommandService commandService)
        {
            foreach (var ev in events)
                ev.Run(commandService);

            base.Run(commandService);
        }

        public void AddEvents()
        {
            var methods = this.GetType().GetMethods()
                .Where(m => m.GetCustomAttributes(typeof(BotEventAttribute), false).Any())
                .ToArray();

            foreach (var method in methods)
                events.Add(new BotEvent(this, method));
        }

        protected bool CheckEvent(Discord.Server server)
        {
            return server == null ? true : IsBlocked((long)server.Id);
        }
    }
}
