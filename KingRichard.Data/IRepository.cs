using System;
using System.Collections.Generic;

namespace KingRichard.Data
{
    public interface IRepository<TEntity>
    where TEntity : class
    {
        void Remove(TEntity item);
        void RemoveById(long id);
        void Add(TEntity item);
        void Update(TEntity item);

        TEntity GetById(long id);
        IEnumerable<TEntity> Get();
        IEnumerable<TEntity> Get(Func<TEntity, bool> predicate);
    }
}
