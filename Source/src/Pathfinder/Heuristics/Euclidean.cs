using Pathfinder.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Pathfinder.Heuristics
{
    public class Euclidean : IHeuristic
    {
        public double Calc(int dx, int dy)
        {
            return Math.Sqrt(dx * dx + dy * dy);
        }
    }
}