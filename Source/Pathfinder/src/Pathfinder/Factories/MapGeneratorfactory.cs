using Pathfinder.Abstraction;
using Pathfinder.MapGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pathfinder.Factories
{
    public class MapGeneratorFactory
    {
        public static IMapGenerator GetFileMapGeneratorImplementation()
        {
            return new FileMapGenerator();
        }

        public static IMapGenerator GetStaticMapGeneratorImplementation()
        {
            return new StaticMapGenerator();
        }

        public static IMapGenerator GetRandomMapGeneratorImplementation()
        {
            return new RandomMapGenerator();
        }
    }
}
