using Pathfinder.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pathfinder
{
    public class Adaptation
    {
        IMap Map;
        public Adaptation(IMap map)
        {
            Map = map;
        }

        public List<Node> Calc(List<Node> listnode)
        {
            return listnode;
        }

    }
}
