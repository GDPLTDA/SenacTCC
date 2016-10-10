using Pathfinder.Abstraction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Pathfinder.MapGenerators
{
    public class RandomMapGenerator: IMapGenerator
    {
        public List<Node> GridMap = new List<Node>();

        public IMap ReadMap(string argument)
        {
            int width = 15, height = 15;
            double seed = 0.2;
            var nodes = new List<Node>();
            var ret = new Map(width, height);


            int size = Convert.ToInt32((width * height) * seed);

            var rand = new Random();
            while (size > 0)
            {

                var p = RandNode(rand, width, height, true);

                GridMap.Add(p);
                size--;
                
            }

            ret.DefineAllNodes(GridMap);
            ret.StartNode = RandNode(rand, width, height, false);
            ret.EndNode = RandNode(rand, width, height, false);
            
           


            if (!ret.ValidMap())
                throw new Exception("Invalid map configuration");

            return ret;
            
        }
        

        private Node RandNode(Random rand, int width, int height , bool wall)
        {
            Node p = null;

            while (p == null || GridMap.Exists(i => i.Equals(p)))
            {
                int x = rand.Next(0, width);
                int y = rand.Next(0, height);
                p = new Node(x, y) { Walkable = !wall };
            }

            return p;
        }


    }
}