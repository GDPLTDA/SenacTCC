using Pathfinder.Abstraction;
using Pathfinder.Factories;
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

        public IMap DefineMap(string argument)
        {
            int width = 15, height = 15;
            double seed = 0.30;
            int minPathLength = 10;
            bool IsAGoodMap = false;
            IMap ret = null;

            // finder para valida se o mapa é passavel
            IFinder AStar = FinderFactory.GetAStarImplementation(
                                    Constants.DiagonalMovement.OnlyWhenNoObstacles,
                                    HeuristicFactory.GetOctileImplementation()
                                );
            

            while (!IsAGoodMap)
            {
                var nodes = new List<Node>();
                var _map = new Map(width, height);


                int size = Convert.ToInt32((width * height) * seed);

                var rand = new Random();
                while (size > 0)
                {

                    var p = RandNode(rand, width, height, true);

                    GridMap.Add(p);
                    size--;

                }

                _map.DefineAllNodes(GridMap);
                _map.StartNode = RandNode(rand, width, height, false);
                _map.EndNode = RandNode(rand, width, height, false);


                if (!_map.ValidMap())
                    throw new Exception("Invalid map configuration");

                if (AStar.Find(_map)) // verifica se o mapa possui um caminho
                {
                    var path = AStar.GetPath();
                    if (path.Max(e=>e.G) >= minPathLength) // verifica se o caminho sastifaz o tamanho minimo
                    {
                        IsAGoodMap = true;
                        ret = _map;
                    }

                }
                GridMap = new List<Node>();

            }

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