using System;

namespace KingRichard.Data
{
    public class ApplicationLogService
    {
        #region Members

        private static readonly Lazy<ApplicationLogService> mLazy =
            new Lazy<ApplicationLogService>(() => new ApplicationLogService());
        private IUserLogRepository<ApplicationLog> mRepository;

        #endregion

        #region Properties

        public static ApplicationLogService Instance
        {
            get
            {
                return mLazy.Value;
            }
        }

        public IUserLogRepository<ApplicationLog> Repository
        {
            get
            {
                return mRepository;
            }
        }

        #endregion

        private ApplicationLogService()
        {
            ProjectRichardDBEntities context = new ProjectRichardDBEntities();
            mRepository = new ApplicationLogRepository(context);
        }
    }
}
