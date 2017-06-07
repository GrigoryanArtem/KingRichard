using KingRichard.Data.BlockedComponents;
using System;

namespace KingRichard.Data
{
    public class BlockedCommandService
    {
        #region Members

        private static readonly Lazy<BlockedCommandService> mLazy =
            new Lazy<BlockedCommandService>(() => new BlockedCommandService());
        private IBlockedCommandRepository mRepository;

        #endregion

        #region Properties

        public static BlockedCommandService Instance
        {
            get
            {
                return  mLazy.Value;
            }
        }

        public IBlockedCommandRepository Repository
        {
            get
            {
                return mRepository;
            }
        }

        #endregion

        private BlockedCommandService()
        {
            ProjectRichardDBEntities context = new ProjectRichardDBEntities();
            mRepository = new BlockedCommandRepository(context);
        }
    }
}
