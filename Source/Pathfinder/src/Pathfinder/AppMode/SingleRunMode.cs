using Pathfinder.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pathfinder.AppMode
{
    public class SingleRunMode : IAppMode
    {
        public void Run()
         {
            var settings = Program.Settings;

            var heuristic = settings.GetHeuristic();
            var finder = settings.GetFinder(heuristic);
            var generator = settings.GetGenerator();
            var viewer = settings.GetViewer(finder);

            var map = generator.DefineMap();
            viewer.Run(map);
        }
    }
}
