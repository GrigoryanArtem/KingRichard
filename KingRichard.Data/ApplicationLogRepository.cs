using System.Collections.Generic;
using System.Data.Entity;

namespace KingRichard.Data
{
    public class ApplicationLogRepository : Repository<ApplicationLog>, IUserLogRepository<ApplicationLog>
    {
        public ApplicationLogRepository(DbContext context) : base(context) { }

        public IEnumerable<ApplicationLog> GetByUserId(long id)
        {
            return Get(app => app.UserId == id);
        }
    }
}
