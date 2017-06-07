using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace KingRichard.Data
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        #region Members

        protected DbContext mContext;
        protected DbSet<TEntity> mDbSet;

        #endregion

        public Repository(DbContext context)
        {
            mContext = context;
            mDbSet = context.Set<TEntity>();
        }

        #region Public methods

        public IEnumerable<TEntity> Get()
        {
            return mDbSet.AsNoTracking().ToList();
        }

        public IEnumerable<TEntity> Get(Func<TEntity, bool> predicate)
        {
            return mDbSet.AsNoTracking().Where(predicate).ToList();
        }

        public TEntity GetById(long id)
        {
            return mDbSet.Find(id);
        }

        public void Add(TEntity item)
        {
            mDbSet.Add(item);
            mContext.SaveChanges();
        }

        public void Update(TEntity item)
        {
            mContext.Entry(item).State = EntityState.Modified;
            mContext.SaveChanges();
        }
        public void Remove(TEntity item)
        {
            mDbSet.Remove(item);
            mContext.SaveChanges();
        }

        public void RemoveById(long id)
        {
            mDbSet.Remove(mDbSet.Find(id));
            mContext.SaveChanges();
        }

        #endregion
    }
}
