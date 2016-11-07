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
                var neighbors = (List<Node>)Map.GetNeighbors(lastcoor, false);

                var coor = neighbors.Find(o => o.Direction == listnode[i].Direction);

                if (coor != null && !newbaby.Exists(x => x.Equals(coor)))
                {
                    ng = ng + ((lastcoor.X - coor.X == 0 || lastcoor.Y - coor.Y == 0) ? 1 : sqrt2);
                    coor.G = ng;
                    newbaby.Add(coor);
                }

                if (lastcoor.Equals(Map.EndNode))
                    break;
            }

            lastcoor = newbaby.Last();
            if (lastcoor.Equals(Map.EndNode))
                return new Genome(Map, newbaby);

            var list = Map.GetNeighbors(lastcoor, false);
            var ind = Settings.Random.Next(0, list.Count);
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

            var ret = new Genome(Map, newbaby);
            //RealignGenome(ret);
            return ret;
        }


        public void RealignGenome(IGenome baby)
        {
            var nodes = baby.ListNodes;
            var parent = nodes.First();
            var nodeCount = nodes.Count();
            for (int i = 1; i < nodeCount; i++)
            {
                var current = nodes[i];

                current.ParentNode = parent;

                if (current.Direction == Constants.DirectionMovement.Up ||
                    current.Direction == Constants.DirectionMovement.UpLeft ||
                    current.Direction == Constants.DirectionMovement.UpRight
                    )
                    current.Y = parent.Y - 1;

                if (current.Direction == Constants.DirectionMovement.Down ||
                    current.Direction == Constants.DirectionMovement.DownLeft ||
                    current.Direction == Constants.DirectionMovement.DownRight
                    )
                    current.Y = parent.Y + 1;


                if (current.Direction == Constants.DirectionMovement.Left ||
                    current.Direction == Constants.DirectionMovement.DownLeft ||
                    current.Direction == Constants.DirectionMovement.UpLeft
                    )
                    current.X = parent.X - 1;

                if (current.Direction == Constants.DirectionMovement.Right ||
                    current.Direction == Constants.DirectionMovement.DownRight ||
                    current.Direction == Constants.DirectionMovement.UpRight
                    )
                    current.X = parent.X - 1;

                parent = current;
            }
        }
    }
}
