using Discord.Commands;
using System.Collections.Generic;
using System;

namespace KingRichard.Bot.Modules
{
    public class DefaultModulesController : IModulesController
    {
        #region Members

        private List<IModule> mModules = new List<IModule>();
        private CommandService mCommandService;

        public IModule[] Modules
        {
            get
            {
                return mModules.ToArray();
            }
        }

        #endregion

        #region Public methods

        public void Init(CommandService commandService)
        {
            mCommandService = commandService;
        }

        public void Add(IModule module)
        {
            mModules.Add(module);
        }

        public void Remove(IModule module)
        {
            mModules.Remove(module);
        }

        public void Run()
        {
            foreach (var module in mModules)
                module.Run(mCommandService);
        }

        #endregion
    }
}
