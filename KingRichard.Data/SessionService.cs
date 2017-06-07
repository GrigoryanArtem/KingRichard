using System;

namespace KingRichard.Data
{
    public class SessionService
    {
        #region Members

        private static readonly Lazy<SessionService> mLazy =
            new Lazy<SessionService>(() => new SessionService());
        private IRepository<Session> mRepository;

        #endregion

        #region Properties

        public static SessionService Instance
        {
            get
            {
                return mLazy.Value;
            }
        }

        public IRepository<Session> Repository
        {
            get
            {
                return mRepository;
            }
        }

        #endregion

        private SessionService()
        {
            ProjectRichardDBEntities context = new ProjectRichardDBEntities();
            mRepository = new SessionRepository(context);
        }
    }
}
