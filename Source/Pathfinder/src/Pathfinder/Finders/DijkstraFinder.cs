using Pathfinder.Abstraction;
using Pathfinder.Factories;
using System;
using System.Linq;
using static System.Math;
namespace Pathfinder.Finders
{
    public class DijkstraFinder : AStarFinder
    {
        public DijkstraFinder(
            DiagonalMovement diag,
            IHeuristic heuristic,
            int weight = 1
          ) : base(diag,heuristic,weight)
        {
            Name = "Dijkstra";
        }
        public override double CalcH(int dx, int dy)
        {
            return 0.0f;
        }
    }
}