using Pathfinder.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pathfinder.Selection
{
    public class SelectionSimple : ISelection
    {
        public List<Node> Select(List<Genome> listnode)
        {
            return new List<Node>();
        }
    }
}
