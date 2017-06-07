using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace KingRichard.Data
{
    public class FilterRepository : Repository<Filter>, IFilterRepository
    {
        public FilterRepository(DbContext context) : base(context) { }

        public IEnumerable<Filter> GetFiltersByGuildId(long guildId)
        {
            return Get(filter => filter.GuildId == guildId);
        }

        public void RemoveFilterByGuildId(long id, long guildId)
        {
            if (GetById(id).GuildId == guildId)
                RemoveById(id);
        }
    }
}
