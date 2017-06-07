using Discord.Commands;

namespace KingRichard.Bot.Modules
{
    public interface IComponent
    {
        string Name { get; }
        IModule ParentModule { get; set; }

        void Run(CommandService commandService);
        bool IsBlocked(long guildId);
    }
}
