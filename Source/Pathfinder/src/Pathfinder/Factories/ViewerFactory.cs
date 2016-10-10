using Pathfinder.Abstraction;
using Pathfinder.Constants;
using Pathfinder.Finders;
using Pathfinder.Viewer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pathfinder.Factories
{
    public class ViewerFactory
    {
        public static IViewer GetConsoleViewerImplementation(IFinder finder)
        {
            return new ConsoleViewer(finder);
        }

        public static IViewer GetOpenGlViewerImplementation(IFinder finder)
        {
            return new OpenGlViewer(finder);
        }
    }
}
