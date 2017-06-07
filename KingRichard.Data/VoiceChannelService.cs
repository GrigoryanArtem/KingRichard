using System;

namespace KingRichard.Data
{
    public class VoiceChannelService
    {
        #region Members

        private static readonly Lazy<VoiceChannelService> mLazy =
            new Lazy<VoiceChannelService>(() => new VoiceChannelService());
        private IRepository<VoiceChannel> mRepository;

        #endregion

        #region Properties

        public static VoiceChannelService Instance
        {
            get
            {
                return mLazy.Value;
            }
        }

        public IRepository<VoiceChannel> Repository
        {
            get
            {
                return mRepository;
            }
        }

        #endregion

        private VoiceChannelService()
        {
            ProjectRichardDBEntities context = new ProjectRichardDBEntities();
            mRepository = new VoiceChannelRepository(context);
        }
    }
}
