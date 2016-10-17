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

        public IGenome Calc(IGenome genome)
        {
            var listnode = genome.ListNodes;
            var newbaby = new List<Node>();
            newbaby.Add(listnode.First());

            for (int i = 1; i < listnode.Count; i++)
            {
                var coor = Map.GetDirectionNode(newbaby.Last(), listnode[i].Direction);

                if (coor!=null && !newbaby.Exists(x => x.Equals(coor)))
                    newbaby.Add(coor);

                if (newbaby.Last().Equals(Map.EndNode))
                    break;
            }

            if (newbaby.Last().Equals(Map.EndNode))
                return new Genome(Map, newbaby);

            var newcoor = Map.GetDirectionNode(newbaby.Last());

            if(newcoor == null)
                return new Genome(Map, newbaby);

            if (!newbaby.Exists(i => i.Equals(newcoor)))
                newbaby.Add(new Node(newcoor));

            return new Genome(Map, newbaby);
        }

    }
}
