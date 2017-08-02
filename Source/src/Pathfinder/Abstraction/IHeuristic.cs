using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Pathfinder.Abstraction
{
   public interface IHeuristic
    {
        double Calc(int dx, int dy);
    }
}