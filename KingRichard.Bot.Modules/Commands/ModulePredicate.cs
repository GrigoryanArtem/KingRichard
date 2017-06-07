using System;

namespace KingRichard.Bot.Modules.Commands
{
    public static class ModulePredicate
    {

        public static Func<IComponent, long, bool> All
        {
            get
            {
                return AllComponents;
            }
        }

        public static Func<IComponent, long, bool> Blocked
        {
            get
            {
                return BlockedComponents;
            }
        }

        public static Func<IComponent, long, bool> Unblocked
        {
            get
            {
                return UnblockedComponents;
            }
        }


        #region private methods

        private static bool AllComponents(IComponent component, long GuildID)
        {
            return true;
        }

        private static bool BlockedComponents(IComponent component, long GuildID)
        {
            return (component is ICommand) ? component.IsBlocked(GuildID) : true;
        }

        private static bool UnblockedComponents(IComponent component, long GuildID)
        {
            return (component is ICommand) ? !component.IsBlocked(GuildID) : true;
        }

        #endregion
    }
}
