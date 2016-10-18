using Pathfinder.Abstraction;
using Pathfinder.Fitness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pathfinder.Factories
{
    public class FitnessFactory
    {
        public static IFitness GetSimpleImplementation()
        {
            return new FitnessHeuristic();
        }
    }
}
