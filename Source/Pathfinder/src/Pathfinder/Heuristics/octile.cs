using Pathfinder.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Pathfinder.Heuristics
{
    public class Octile : IHeuristic
    {
        public double Calc(int dx, int dy)
        {
            var F = Math.Sqrt(2) - 1;
            return (dx < dy) ? F * dx + dy : F * dy + dx;
        }
    }
}