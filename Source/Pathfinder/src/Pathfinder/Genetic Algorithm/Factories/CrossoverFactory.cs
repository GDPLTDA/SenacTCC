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
        public static ICrossover GetSimpleImplementation(double rate)
        {
            return new CrossoverSimple(rate);
        }
        public static ICrossover GetOBXImplementation(double rate)
        {
            return new CrossoverOBX(rate);
        }
        public static ICrossover GetPBXImplementation(double rate)
        {
            return new CrossoverPBX(rate);
        }
    }
}
