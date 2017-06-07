using KingRichard.Data.BlockedComponents;
using System;

namespace KingRichard.Data
{
    public class BlockedModuleService
    {
        #region Members

        private static readonly Lazy<BlockedModuleService> mLazy =
            new Lazy<BlockedModuleService>(() => new BlockedModuleService());
        private IBlockedModuleRepository mRepository;

        #endregion

        #region Properties

        public static BlockedModuleService Instance
        {
            get
            {
                return mLazy.Value;
            }
        }

        public IBlockedModuleRepository Repository
        {
            get
            {
                return mRepository;
            }
        }

        #endregion

        private BlockedModuleService()
        {
            ProjectRichardDBEntities context = new ProjectRichardDBEntities();
            mRepository = new BlockedModuleRepository(context);
        }
    }
}
