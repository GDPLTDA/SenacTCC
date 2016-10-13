using Pathfinder.Abstraction;
using Pathfinder.AppMode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pathfinder.Factories
{
    public class AppModeFactory
    {
        public static IAppMode GetSingleRunImplementation()
        {
            return new SingleRunMode();
        }

        public static IAppMode GetDynamicImplementation()
        {
            return new DynamicMode();
        }

        public static IAppMode GetBatchImplementation()
        {
            return new BatchMode();
        }
    }
}
