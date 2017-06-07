namespace KingRichard.Bot.Modules
{
    public interface ICommand : IComponent
    {
        BotRoles Role { get; }
    }
}
