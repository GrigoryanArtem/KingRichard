using System.Linq;
using System.Data.Entity;

namespace KingRichard.Data.BlockedComponents
{
    public class BlockedCommandRepository : Repository<BlockedCommand>, IBlockedCommandRepository
    {
        public BlockedCommandRepository(DbContext context) : base(context) { }

        public BlockedCommand GetByName(string name, long guildId)
        {
            return Get(command => command.CommandName == name && command.GuildId == guildId).FirstOrDefault();
        }
    }
}
