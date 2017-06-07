using System.Data.Entity;

namespace KingRichard.Data
{
    public class VoiceChannelRepository : Repository<VoiceChannel>
    {
        public VoiceChannelRepository(DbContext context) : base(context) { }
    }
}
