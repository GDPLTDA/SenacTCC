using Pathfinder.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pathfinder.Selection
{
    public class SelectionRandom : ISelection
    {
        public IGenome Select(List<IGenome> listnode)
        {
            var rand = GAResolver.Resolve<IRandom>();
            var ind = rand.Next(0, listnode.Count);

            return listnode[ind];
        }
    }
}
