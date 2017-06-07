namespace KingRichard.Data.BlockedComponents
{
    public interface IBlockedComponentRepository<T>
    {
        T GetByName(string name, long guildId);
    }
}
