using System;

namespace KingRichard.Data
{
    public class VoiceChannelLogService
    {
        #region Members

        private static readonly Lazy<VoiceChannelLogService> mLazy =
            new Lazy<VoiceChannelLogService>(() => new VoiceChannelLogService());
        private IUserLogRepository<VoiceChannelLog> mRepository;

        #endregion

        #region Properties

        public static VoiceChannelLogService Instance
        {
            get
            {
                return mLazy.Value;
            }
        }

        public IUserLogRepository<VoiceChannelLog> Repository
        {
            get
            {
                return mRepository;
            }
        }

        #endregion

        private VoiceChannelLogService()
        {
            ProjectRichardDBEntities context = new ProjectRichardDBEntities();
            mRepository = new VoiceChannelLogRepository(context);
        }
    }
}
