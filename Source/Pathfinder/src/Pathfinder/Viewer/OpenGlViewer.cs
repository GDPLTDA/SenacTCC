

using OpenTK.Graphics;
using Pathfinder.Abstraction;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Pathfinder.Viewer
{
    public class OpenGlViewer : IViewer
    {
        
        OpenGlWindow window;
        IFinder _finder;

        public OpenGlViewer(IFinder finder)
        {
            _finder = finder;
        }

        public void End(FinderEventArgs e)
        {
           
        }

        public void Loop(FinderEventArgs e)
        {
            
        }

        public void Run(IMap map)
        {
            var set = new Settings();
            window = new OpenGlWindow(map, _finder,set.OpenGlBlockSize );
            window.Run();
        }

        public void Start()
        {
           
        }
    }
}
