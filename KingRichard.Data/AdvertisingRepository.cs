using System.Data.Entity;

namespace KingRichard.Data
{
    public class AdvertisingRepository : Repository<Advertising>
    {
        public AdvertisingRepository(DbContext context) : base(context) { }
    }
}
