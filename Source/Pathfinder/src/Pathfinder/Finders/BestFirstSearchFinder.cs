using Pathfinder.Abstraction;
using Pathfinder.Constants;
using Pathfinder.Factories;
using System;
using System.Linq;
using static System.Math;

namespace Pathfinder.Finders
{
    public class BestFirstSearchFinder : AStarFinder
    {
     
        public BestFirstSearchFinder(
            DiagonalMovement diag,
            IHeuristic heuristic,
            int weight = 1
          ) : base(diag,heuristic,weight)
        {

        }

        public override double CalcH(int dx, int dy)
        {
            return base.CalcH(dx, dy) * 1000000;
        }

    }
}
