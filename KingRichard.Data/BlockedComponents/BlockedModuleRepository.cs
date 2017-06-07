using System.Data.Entity;
using System.Linq;

namespace KingRichard.Data.BlockedComponents
{
    public class BlockedModuleRepository : Repository<BlockedModule>, IBlockedModuleRepository
    {
        public BlockedModuleRepository(DbContext context) : base(context) { }

        public BlockedModule GetByName(string name, long guildId)
        {
            return Get(command => command.ModuleName == name && command.GuildId == guildId).FirstOrDefault();
        }
    }
}
