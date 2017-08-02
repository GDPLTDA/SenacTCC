using Pathfinder.Abstraction;
using Pathfinder.Heuristics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Pathfinder.Factories
{
    public class HeuristicFactory : IFactory<IHeuristic>
    {
        public static IHeuristic GetManhattamImplementation()
            => new Manhattan();
        public static IHeuristic GetEuclideanImplementation()
            => new Euclidean();
        public static IHeuristic GetOctileImplementation()
            => new Octile();
        public static IHeuristic GetChebyshevImplementation()
            => new Chebyshev();
        public IHeuristic GetImplementation()
            => Decide(Settings.Heuristic);
        public IHeuristic GetImplementation(int option)
            => Decide((HeuristicEnum)option);
        private static IHeuristic Decide(HeuristicEnum option)
        {
            switch (option)
            {
                case HeuristicEnum.Manhattan:
                    return GetManhattamImplementation();
                case HeuristicEnum.Euclidean:
                    return GetEuclideanImplementation();
                case HeuristicEnum.Octile:
                    return GetOctileImplementation();
                case HeuristicEnum.Chebyshev:
                    return GetChebyshevImplementation();
            }
            throw new Exception("No Heuristic selected");
        }
    }
}