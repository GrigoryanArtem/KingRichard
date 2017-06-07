using System;

namespace KingRichard.Bot.Modules
{
    public class PermissionControllerService
    {
        private static readonly Lazy<PermissionControllerService> mLazy = new Lazy<PermissionControllerService>(() => new PermissionControllerService());
        private IPermissionController mPermissionController;

        public static PermissionControllerService Instance
        {
            get
            {
                return mLazy.Value;
            }
        }

        private PermissionControllerService()
        {
            mPermissionController = new PermissionController();
        }

        public IPermissionController Create()
        {
            return mPermissionController;
        }
    }
}
