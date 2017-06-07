using Discord.Commands;
using KingRichard.Bot.Modules.Attributes;
using System.Reflection;

namespace KingRichard.Bot.Modules
{
    public class Permission : IPermission
    {
        protected MethodInfo mMethod;
        protected IModule mParentModule;
        private string mMessage;

        public Permission(IModule parentModule, MethodInfo checkMethod)
        {
            mMethod = checkMethod;
            mParentModule = parentModule;
            mMessage = mMethod.GetCustomAttribute<PermissionAttribute>().Message;
        }

        public void Check(CommandEventArgs args, ICommand command)
        {
            if (!((bool)mMethod.Invoke(mParentModule, new object[] { args, command })))
                throw new PermissionException(mMessage);
        }
    }
}
