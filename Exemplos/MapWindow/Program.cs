using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenTK;

namespace MapWindow
{
    static class Program
    {
        static void Main(string[] args)
        {
            MapWindow Window = new MapWindow(620,480);
            Window.Run();
        }
        
    }
}
