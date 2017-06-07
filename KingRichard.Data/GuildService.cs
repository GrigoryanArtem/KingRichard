using System;

namespace KingRichard.Data
{
    public class GuildService
    {
        #region Members

        private static readonly Lazy<GuildService> mLazy =
            new Lazy<GuildService>(() => new GuildService());
        private Repository<Guild> mRepository;

        #endregion

        #region Properties

        public static GuildService Instance
        {
            get
            {
                return mLazy.Value;
            }
        }

        public Repository<Guild> Repository
        {
            get
            {
                return mRepository;
            }
        }

        #endregion

        private GuildService()
        {
            ProjectRichardDBEntities context = new ProjectRichardDBEntities();
            mRepository = new GuildRepository(context);
        }
    }
}
