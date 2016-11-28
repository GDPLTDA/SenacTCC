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
            
            var heuristic = Container.Resolve<IHeuristic>();
            var finder = Container.Resolve<IFinder>();
            var generator = Container.Resolve<IMapGenerator>();
            var viewer = Container.Resolve<IViewer>();
            
            var map = generator.DefineMap();
            viewer.SetFinder(finder);
            viewer.Run(map);
        }
    }
}
