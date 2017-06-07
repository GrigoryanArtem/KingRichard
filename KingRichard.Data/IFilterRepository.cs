using System.Collections.Generic;

namespace KingRichard.Data
{
    public interface IFilterRepository : IRepository<Filter>
    {
        IEnumerable<Filter> GetFiltersByGuildId(long guildId);
        void RemoveFilterByGuildId(long id, long guildId);
    }
}
