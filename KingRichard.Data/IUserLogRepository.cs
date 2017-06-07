using System.Collections.Generic;

namespace KingRichard.Data
{
    public interface IUserLogRepository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        IEnumerable<TEntity> GetByUserId(long id);
    }
}
