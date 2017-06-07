namespace KingRichard.Bot.Modules
{
    public interface IModule : IComponent
    {
        string Description { get; }
        IComponent[] Components { get; }
    }
}
