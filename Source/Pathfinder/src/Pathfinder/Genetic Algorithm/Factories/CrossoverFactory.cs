using Pathfinder.Abstraction;
using Pathfinder.Crossover;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pathfinder.Factories
{
    public class CrossoverFactory
    {
        public static ICrossover GetSimpleImplementation()
        {
            return new CrossoverSimple();
        }
        public static ICrossover GetOBXImplementation()
        {
            return new CrossoverOBX();
        }
        public static ICrossover GetPBXImplementation()
        {
            return new CrossoverPBX();
        }
    }
}
