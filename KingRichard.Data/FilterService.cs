using System;

namespace KingRichard.Data
{
    public class FilterService
    {
        #region Members

        private static readonly Lazy<FilterService> mLazy =
            new Lazy<FilterService>(() => new FilterService());
        private IFilterRepository mRepository;

        #endregion

        #region Properties

        public static FilterService Instance
        {
            get
            {
                return mLazy.Value;
            }
        }

        public IFilterRepository Repository
        {
            get
            {
                return mRepository;
            }
        }

        #endregion

        private FilterService()
        {
            ProjectRichardDBEntities context = new ProjectRichardDBEntities();
            mRepository = new FilterRepository(context);
        }
    }
}
