using Pathfinder.Abstraction;
using Pathfinder.Heuristics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pathfinder.Factories
{
    public class HeuristicFactory
    {
        public static IHeuristic GetManhattamImplementation()
        {
            return new Manhattan();
        }

        public static IHeuristic GetEuclideanImplementation()
        {
            return new Euclidean();
        }

        public static IHeuristic GetOctileImplementation()
        {
            return new Octile();
        }

        public static IHeuristic GetChebyshevImplementation()
        {
            return new Chebyshev();
        }


    }
}
