using Pathfinder.Abstraction;
using System.Collections.Generic;

namespace Pathfinder.Selection
{
    public class SelectionRandom : ISelection
    {
        public IGenome Select(List<IGenome> listnode)
        {
            var ind = Settings.Random.Next(0, listnode.Count);

            return listnode[ind];
        }
    }
}
