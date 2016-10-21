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
            var sqrt2 = Math.Sqrt(2);
            double ng = 0;
            Node lastcoor;
            for (int i = 1; i < listnode.Count; i++)
            {
                lastcoor = newbaby.Last();
                var coor = Map.GetDirectionNode(lastcoor, listnode[i].Direction, false);

                if (coor != null && !newbaby.Exists(x => x.Equals(coor)))
                {
                    ng = ng + ((lastcoor.X - coor.X == 0 || lastcoor.Y - coor.Y == 0) ? 1 : sqrt2);
                    coor.G = ng;
                    newbaby.Add(coor);
                }

                if (newbaby.Last().Equals(Map.EndNode))
                    break;
            }

            lastcoor = newbaby.Last();
            if (lastcoor.Equals(Map.EndNode))
                return new Genome(Map, newbaby);

            var list = Map.GetNeighbors(lastcoor, false);
            var ind = Settings.Random.Next(0, list.Count);
            var newnode = list[ind];

            if (!newbaby.Exists(i=>i.EqualsAll(newnode)))
            {
                ng = ng + ((lastcoor.X - newnode.X == 0 || lastcoor.Y - newnode.Y == 0) ? 1 : sqrt2);
                newnode.G = ng;
                newbaby.Add(new Node(newnode));
            }

            return new Genome(Map, newbaby);
        }

    }
}
