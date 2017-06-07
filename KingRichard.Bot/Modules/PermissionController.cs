using System.Collections.Generic;
using Discord.Commands;
using System.Threading.Tasks;

namespace KingRichard.Bot.Modules
{
    public class PermissionController : IPermissionController
    {
        private List<IPermission> mPermissions = new List<IPermission>();

        public void Add(IPermission permission)
        {
            mPermissions.Add(permission);
        }

        public void Check(CommandEventArgs args, ICommand command)
        {
            foreach (var permission in mPermissions)
                permission.Check(args, command);
        }

        public void Remove(IPermission permission)
        {
            mPermissions.Remove(permission);
        }
    }
}
