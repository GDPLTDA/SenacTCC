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
    public class ViewerFactory : IFactory<IViewer>
    {
        public IViewer GetConsoleViewerImplementation()
         => new ConsoleViewer();
        
        public IViewer GetOpenGlViewerImplementation()
            => new OpenGlViewer();
        
        public IViewer GetImplementation()
            => Decide(Settings.MapViwer);

        public IViewer GetImplementation(int option)
            => Decide((ViewerEnum)option);

        public IViewer Decide(ViewerEnum option)
        {
            switch (option)
            {
                case ViewerEnum.Console:
                    return GetConsoleViewerImplementation();

                case ViewerEnum.OpenGL:
                    return GetOpenGlViewerImplementation();

            }

            throw new Exception("No viewer selected");

        }
    }
}
