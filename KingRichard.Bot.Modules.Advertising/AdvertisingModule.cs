using Discord;
using KingRichard.Bot.Modules.Attributes;
using KingRichard.Bot.Modules.Events;
using KingRichard.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KingRichard.Bot.Modules.Advertising
{
    [Module(Constants.AdvertisingModuleName)]
    [Description(Constants.AdvertisingModuleDescription)]
    public class AdvertisingModule : ExtendDefaultModule
    {
        #region Members

        private List<Channel> mChannels = new List<Channel>();
        private Random mRandom = new Random();

        Timer mTimer;

        #endregion

        public AdvertisingModule() : base()
        {
            TimerCallback timerCallback = new TimerCallback(PrintAdvertising);
            mTimer = new Timer(timerCallback, null, Constants.StartTimerTime, Constants.TimerTick);
        }

        #region Events

        [BotEvent(Constants.AddGuildEventName, BotEvents.ServerAvailable)]
        public void AddGuild(object sender, ServerEventArgs args)
        {
            var channel = args.Server.DefaultChannel;

            if (UserService.Instance.Repository.GetById((long)channel.Server.Owner.Id).Type != UserType.Premium && 
                !mChannels.Exists(c => c.Id == channel.Id))
                mChannels.Add(channel);
        }

        [BotEvent(Constants.RemoveGuildEventName, BotEvents.LeftServer)]
        public void RemoveGuild(object sender, ServerEventArgs args)
        {
            if (CheckEvent(args.Server))
                return;

            mChannels.RemoveAll(channel => channel.Id == args.Server.Id);
        }

        #endregion

        #region Private methods

        private async void PrintAdvertising(object obj)
        {
            ClearPremiumAccounts();

            List<Message> messages = new List<Message>();

            foreach (var channel in mChannels)
                messages.Add(await channel.SendMessage(GetAdvertising()));

            await Task.Delay(Constants.Delay);

            foreach (var message in messages)
                await message.Delete();
        }

        private string GetAdvertising()
        {
            var advertisings = AdvertisingService.Instance.Repository.Get();

            int elementNumber = mRandom.Next(advertisings.Count());
            return advertisings.ElementAt(elementNumber).Text;
        }

        private void ClearPremiumAccounts()
        {
            mChannels.RemoveAll(channel => UserService.Instance.Repository.GetById((long)channel.Server.Owner.Id).Type == UserType.Premium);
        }

        #endregion
    }
}
