using Pathfinder.UI.Abstraction;
using Pathfinder.UI.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Pathfinder.Container;
namespace Pathfinder.UI
{
    public class RegisterConfig
    {
        public static void BindProjectRegisters()
        {
            Register<IAppMode, AppModeFactory>();
            Register<IViewer, ViewerFactory>();
        }
    }
}
