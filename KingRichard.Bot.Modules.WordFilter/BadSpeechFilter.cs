using Discord;
using KingRichard.Bot.Modules.Attributes;
using KingRichard.Bot.Modules.Events;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KingRichard.Bot.Modules.WordFilter
{
    [Module(Constants.BadSpeechFilterModuleName)]
    [Description(Constants.BadSpeechFilterModuleDescription)]
    public class BadSpeechFilter : ExtendDefaultModule
    {
        #region Events

        [BotEvent(Constants.ObsceneEventName, BotEvents.MessageUpdated)]
        public async void ObsceneUpdatedEvent(object sender, MessageUpdatedEventArgs args)
        {
            if (CheckEvent(args.Server))
                return;

            try
            {
                await RemoveMessageByObscene(sender, args);
            }
            catch { }
        }

        [BotEvent(Constants.ObsceneEventName, BotEvents.MessageReceived)]
        public async void ObsceneReceivedEvent(object sender, MessageEventArgs args)
        {
            if (CheckEvent(args.Server))
                return;

            try
            {
                await RemoveMessageByObscene(sender, args);
            }
            catch { }
        }

        #endregion

        private async Task RemoveMessageByObscene(object sender, MessageUpdatedEventArgs args)
        {
            if (CheckEvent(args.Server))
                return;

            if (!args.User.IsBot && CheckMessegeByObscene(args.After.Text))
            {
                await args.After.Delete();
                var botAnswer = await args.Channel.SendMessage(Strings.RemoveMessegeByObscene);

                await Task.Delay(Constants.Delay);
                await botAnswer.Delete();
            }
        }

        private async Task RemoveMessageByObscene(object sender, MessageEventArgs args)
        {
            if (CheckEvent(args.Server))
                return;

            if (!args.User.IsBot && CheckMessegeByObscene(args.Message.Text))
            {
                await args.Message.Delete();
                var botAnswer = await args.Channel.SendMessage(Strings.RemoveMessegeByObscene);

                await Task.Delay(Constants.Delay);
                await botAnswer.Delete();
            }
        }

        private static bool CheckMessegeByObscene(string text)
        {
            string pattern = Strings.ObsceneWordsRegex;
            Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);

            bool answer = rgx.IsMatch(text);
            return answer;
        }

    }
}
