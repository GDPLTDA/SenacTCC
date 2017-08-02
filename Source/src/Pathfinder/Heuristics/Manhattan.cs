using Pathfinder.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Pathfinder.Heuristics
{
    public class Manhattan : IHeuristic
    {
        public double Calc(int dx, int dy)
        {
            return dx + dy;
        }
    }
}