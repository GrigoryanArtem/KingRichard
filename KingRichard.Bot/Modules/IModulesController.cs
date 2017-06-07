using Discord.Commands;

namespace KingRichard.Bot.Modules
{
    public interface IModulesController : IModulesObserver
    {
        void Init(CommandService commandService);
        void Run();
    }
}
