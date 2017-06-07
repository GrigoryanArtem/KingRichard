using System.Data.Entity;

namespace KingRichard.Data
{
    public class GuildRepository : Repository<Guild>
    {
        public GuildRepository(DbContext context) : base(context) { }
    }
}
