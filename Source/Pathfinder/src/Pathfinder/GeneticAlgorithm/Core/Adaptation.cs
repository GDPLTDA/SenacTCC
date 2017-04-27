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
            var rand = Container.Resolve<IRandom>();
            var listnode = genome.ListNodes;

            var startnode = listnode.First();
            if (!startnode.Equals(Map.StartNode))
                startnode = Map.StartNode;
            var newbaby = new List<Node>
            {
                startnode
            };
            var sqrt2 = Math.Sqrt(2);
            double ng = 0;
            Node lastcoor;
            for (int i = 1; i < listnode.Count; i++)
            {
                lastcoor = newbaby.Last();
                var neighbors = (List<Node>)Map.GetNeighbors(lastcoor, false);

                var coor = neighbors.Find(o => o.Direction == listnode[i].Direction);

                if (coor != null && !newbaby.Exists(x => x.Equals(coor)))
                {
                    var dx = lastcoor.X - coor.X;
                    var dy = lastcoor.Y - coor.Y;

                    if(Math.Abs(dx) > 1 || Math.Abs(dy) > 1)
                    {
                        int f = 0;
                        f = 1;
                        var j = f;
                    }


                    var g = ((dx == 0 || dy == 0) ? 1 : sqrt2);
                    ng = ng + g;
                    coor.G = ng;
                    newbaby.Add(coor);
                }
                if (lastcoor.Equals(Map.EndNode))
                    break;
            }
            lastcoor = newbaby.Last();
            if (lastcoor.Equals(Map.EndNode))
                return new Genome(Map, newbaby);
            var list = Map.GetNeighbors(lastcoor, false, false);
            if (list.Count > 0)
            {
                var ind = rand.Next(0, list.Count);
                var newnode = list[ind];
                if (!newbaby.Exists(i => i.EqualsAll(newnode)))
                {
                    lastcoor.Collision = !newnode.Walkable;
                    if (lastcoor.Collision)
                        return new Genome(Map, newbaby);
                    ng = ng + ((lastcoor.X - newnode.X == 0 || lastcoor.Y - newnode.Y == 0) ? 1 : sqrt2);
                    newnode.G = ng;
                    newbaby.Add(new Node(newnode));
                }
            }
            return new Genome(Map, newbaby);
        }
    }
}