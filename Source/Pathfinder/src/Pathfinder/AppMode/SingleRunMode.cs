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
            
            var heuristic = Resolver.Resolve<IHeuristic>();
            var finder = Resolver.Resolve<IFinder>();
            var generator = Resolver.Resolve<IMapGenerator>();
            var viewer = Resolver.Resolve<IViewer>();
            
            var map = generator.DefineMap();
            viewer.SetFinder(finder);
            viewer.Run(map);
        }
    }
}
