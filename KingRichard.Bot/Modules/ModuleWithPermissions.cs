using KingRichard.Bot.Modules.Attributes;
using System.Linq;

namespace KingRichard.Bot.Modules
{
    public abstract class ModuleWithPermissions : DefaultCommandModule
    {
        private IPermissionController mPermissionController;

        public ModuleWithPermissions(IModule parent, IPermissionController permissionController) : base(parent)
        {
            mPermissionController = permissionController;
            AddCommands();
            AddPermissioins();
        }

        public ModuleWithPermissions(IPermissionController permissionController) : this(null, permissionController) { }

        protected override void AddCommands()
        {
            var methods = this.GetType().GetMethods()
                .Where(m => m.GetCustomAttributes(typeof(CommandAttribute), false).Any())
                .ToArray();

            foreach (var method in methods)
                mComponents.Add(new BotCommandWithPermissions(this, method, mPermissionController));
        }

        protected void AddPermissioins()
        {
            var permissions = this.GetType().GetMethods()
                .Where(m => m.GetCustomAttributes(typeof(PermissionAttribute), false).Any())
                .ToArray();

            foreach (var permission in permissions)
                mPermissionController.Add(new Permission(this, permission));
        }
    }
}
