using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Pathfinder.Abstraction
{
    public interface ICrossover
    {
        CrossoverOperation Calc(CrossoverOperation Operation);
    }
}