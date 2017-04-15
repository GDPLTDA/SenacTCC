using Pathfinder.UI.Abstraction;
using Pathfinder.UI.Factories;
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
