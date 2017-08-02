using Pathfinder.Abstraction;
using Pathfinder.MapGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Pathfinder.Factories
{
    public class MapGeneratorFactory : IFactory<IMapGenerator>
    {
        public static IMapGenerator GetFileMapGeneratorImplementation()
            => new FileMapGenerator();
        public static IMapGenerator GetStaticMapGeneratorImplementation()
            => new StaticMapGenerator();
        public static IMapGenerator GetRandomMapGeneratorImplementation()
            => new RandomMapGenerator();
        public static IMapGenerator GetStandardMapGeneratorImplementation()
            => new StandardMapGenerator();
        public IMapGenerator GetImplementation()
            => Decide(Settings.MapOrigin);
        public IMapGenerator GetImplementation(int option)
            => Decide((MapGeneratorEnum)option);
        private static IMapGenerator Decide(MapGeneratorEnum option)
        {
            switch (option)
            {
                case MapGeneratorEnum.File:
                    return GetFileMapGeneratorImplementation();
                case MapGeneratorEnum.Static:
                    return GetStaticMapGeneratorImplementation();
                case MapGeneratorEnum.Random:
                    return GetRandomMapGeneratorImplementation();
                case MapGeneratorEnum.WithPattern:
                    return GetStandardMapGeneratorImplementation();
            }
            throw new Exception("No generator selected");
        }
    }
}