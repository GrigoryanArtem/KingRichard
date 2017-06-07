using System;

namespace KingRichard.Data
{
    public class AdvertisingService
    {
        #region Members

        private static readonly Lazy<AdvertisingService> mLazy =
            new Lazy<AdvertisingService>(() => new AdvertisingService());
        private IRepository<Advertising> mRepository;

        #endregion

        #region Properties

        public static AdvertisingService Instance
        {
            get
            {
                return mLazy.Value;
            }
        }

        public IRepository<Advertising> Repository
        {
            get
            {
                return mRepository;
            }
        }

        #endregion

        private AdvertisingService()
        {
            ProjectRichardDBEntities context = new ProjectRichardDBEntities();
            mRepository = new AdvertisingRepository(context);
        }
    }
}
