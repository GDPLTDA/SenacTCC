using Pathfinder.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pathfinder.Selection
{
    public class SelectionSimple : ISelection
    {
        public IGenome Select(List<IGenome> listnode)
        {
            var ind = Settings.Random.Next(0, listnode.Count);

            return listnode[ind];
        }
    }
}
