using System.Data.Entity;

namespace KingRichard.Data
{
    public class SessionRepository : Repository<Session>
    {
        public SessionRepository(DbContext context) : base(context) { }
    }
}
