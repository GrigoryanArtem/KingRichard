using Discord.Commands;
using System.Threading.Tasks;

namespace KingRichard.Bot.Modules
{
    public interface IPermission
    {
        void Check(CommandEventArgs args, ICommand command);
    }
}
