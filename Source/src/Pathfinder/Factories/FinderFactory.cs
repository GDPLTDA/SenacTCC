using Pathfinder.Abstraction;
using Pathfinder.Finders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Pathfinder.Factories
{
    public class FinderFactory : IFactory<IFinder>
    {
        public static IFinder GetAStarImplementation(DiagonalMovement diag, IHeuristic heuristic)
            => new AStarFinder(diag, heuristic);
        public static IFinder GetBFSImplementation(DiagonalMovement diag, IHeuristic heuristic)
            => new BestFirstSearchFinder(diag, heuristic);
        public static IFinder GetIDAStarImplementation(DiagonalMovement diag, IHeuristic heuristic)
            => new IDAStarFinder(diag, heuristic);
        public static IFinder GetDijkstraImplementation(DiagonalMovement diag, IHeuristic heuristic)
            => new DijkstraFinder(diag, heuristic);
        public static IFinder GetGAImplementation(DiagonalMovement diag)
            => new GAFinder(diag);
        public IFinder GetImplementation()
            => Decide(Settings.Algorithm);
        public IFinder GetImplementation(int option)
            => Decide((FinderEnum)option);
        private static IFinder Decide(FinderEnum option)
        {
            var allowDiagonal = Settings.AllowDiagonal;
            var heuri = Container.Resolve<IHeuristic>();
            switch (option)
            {
                case FinderEnum.AStar:
                    return GetAStarImplementation(allowDiagonal, heuri);
                case FinderEnum.BestFirstSearch:
                    return GetBFSImplementation(allowDiagonal, heuri);
                case FinderEnum.IDAStar:
                    return GetIDAStarImplementation(allowDiagonal, heuri);
                case FinderEnum.Dijkstra:
                    return GetDijkstraImplementation(allowDiagonal, heuri);
                case FinderEnum.GA:
                    return GetGAImplementation(allowDiagonal);
            }
            throw new Exception("No finder selected");
        }
    }
}