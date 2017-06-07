using System;

namespace KingRichard.Data
{
    public class UserService
    {
        #region Members

        private static readonly Lazy<UserService> mLazy =
            new Lazy<UserService>(() => new UserService());
        private Repository<User> mRepository;

        #endregion

        #region Properties

        public static UserService Instance
        {
            get
            {
                return mLazy.Value;
            }
        }

        public Repository<User> Repository
        {
            get
            {
                return mRepository;
            }
        }

        #endregion

        private UserService()
        {
            ProjectRichardDBEntities context = new ProjectRichardDBEntities();
            mRepository = new UserRepository(context);
        }
    }
}
