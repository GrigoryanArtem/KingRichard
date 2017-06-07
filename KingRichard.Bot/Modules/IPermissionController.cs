using Discord.Commands;
using System.Threading.Tasks;

namespace KingRichard.Bot.Modules
{
    public interface IPermissionController
    {
        void Add(IPermission permission);
        void Remove(IPermission permission);
        void Check(CommandEventArgs args, ICommand command);
    }
}
