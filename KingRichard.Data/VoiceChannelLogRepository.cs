using System.Collections.Generic;
using System.Data.Entity;

namespace KingRichard.Data
{
    public class VoiceChannelLogRepository : Repository<VoiceChannelLog>, IUserLogRepository<VoiceChannelLog>
    {
        public VoiceChannelLogRepository(DbContext context) : base(context) { }

        public IEnumerable<VoiceChannelLog> GetByUserId(long id)
        {
            return Get(log => log.UserId == id);
        }
    }
}
