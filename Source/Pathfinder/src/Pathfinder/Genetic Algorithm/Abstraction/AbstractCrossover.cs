using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pathfinder.Abstraction
{
    public class CrossoverOperation
    {
        public CrossoverOperation()
        {

        }

        public CrossoverOperation(List<Node> mon, List<Node> dad)
        {
            Dad = mon;
            Mom = dad;
        }
        List<Node> Mom { get; set; }
        List<Node> Dad { get; set; }
    }
    public abstract class AbstractCrossover : ICrossover
    {
        CrossoverOperation Operation { get; set; }
        public abstract CrossoverOperation Cross(CrossoverOperation Operation);
    }
}
