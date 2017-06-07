namespace KingRichard.Bot.Modules
{
    public interface IModulesObserver
    {
        IModule[] Modules { get; }
        void Add(IModule module);
        void Remove(IModule module);
    }
}
