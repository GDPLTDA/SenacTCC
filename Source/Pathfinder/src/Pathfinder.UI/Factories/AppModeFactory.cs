using Pathfinder.Abstraction;
using Pathfinder.UI.Abstraction;
using Pathfinder.UI.AppMode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Pathfinder.UI.Factories
{
    public class AppModeFactory : IFactory<IAppMode>
    {
        public IAppMode GetSingleRunImplementation()
            => new SingleRunMode();
        public IAppMode GetDynamicImplementation()
             => new DynamicMode();
        public IAppMode GetBatchImplementation()
            => new BatchMode();
        public IAppMode GetImplementation()
            => DecideImplementation(UISettings.AppMode);
        public IAppMode GetImplementation(int option)
            => DecideImplementation((AppModeEnum)option);
        private IAppMode DecideImplementation(AppModeEnum mode)
        {
            switch (mode)
            {
                case AppModeEnum.SingleRun:
                    return GetSingleRunImplementation();
                case AppModeEnum.Dynamic:
                    return GetDynamicImplementation();
                case AppModeEnum.BatchMode:
                    return GetBatchImplementation();
            }
            throw new Exception("No app mode selected");
        }
    }
}