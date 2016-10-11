using Pathfinder.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pathfinder.Crossover
{
    public class CrossoverOBX : AbstractCrossover
    {
        public override CrossoverOperation Cross(CrossoverOperation Operation)
        {
            return new CrossoverOperation();
        }
    }
}
