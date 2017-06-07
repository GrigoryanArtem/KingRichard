using System.Reflection;
using Discord.Commands;

namespace KingRichard.Bot.Modules
{
    public class BotCommandWithPermissions : BotCommand
    {
        private IPermissionController mPermissionController;

        public BotCommandWithPermissions(IModule parentModule, MethodInfo commandMethod, IPermissionController permissionController) 
            : base(parentModule, commandMethod)
        {
            mPermissionController = permissionController;
        }
        
        protected override void InitCommand(CommandEventArgs args)
        {
             if(!(args.User == args.Server.Owner))
                mPermissionController.Check(args, this);
        }
    }
}
