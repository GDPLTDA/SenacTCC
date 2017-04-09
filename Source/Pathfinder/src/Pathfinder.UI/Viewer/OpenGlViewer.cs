

using OpenTK.Graphics;
using Pathfinder.Abstraction;
using Pathfinder.UI.Abstraction;
using System;
using System.Collections.Generic;
using System.Threading;
namespace Pathfinder.UI.Viewer
{
    public class OpenGlViewer : IViewer
    {
        OpenGlWindow window;
        IFinder _finder;
        public OpenGlViewer()
        {
        }
        public void Run(IMap map)
        {
            window = new OpenGlWindow(map, _finder,Settings.OpenGlBlockSize );
            window.Run();
        }
        public void SetFinder(IFinder finder)
        {
            _finder = finder;
        }
    }
}
