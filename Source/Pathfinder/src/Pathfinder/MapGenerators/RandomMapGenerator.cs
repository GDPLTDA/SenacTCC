using Pathfinder.Abstraction;
using Pathfinder.Constants;
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

        public IMap DefineMap(string argument, DiagonalMovement? diagonal = null)
        {
            Settings settings = new Settings();
            int width = settings.Width, height = settings.Height;
            double seed = settings.RandomSeed;
            int minPathLength = settings.MinimumPath;
            bool IsAGoodMap = false;
            IMap ret = null;

            if (argument!=string.Empty)
            {
                var param = argument.Split('|');
                try
                {
                    width   =  int.Parse(param[0]);
                    height  =  int.Parse(param[1]);
                    seed    =  double.Parse(param[2])/100;
                    minPathLength = int.Parse(param[3]);

                }
                catch 
                {

                    throw new Exception("Ivalid random map parameters!");
                }


            }


            DiagonalMovement d = settings.AllowDiagonal; 
            if (diagonal.HasValue)
                d = diagonal.Value;


            // finder para valida se o mapa é passavel
            IFinder AStar = FinderFactory.GetBFSImplementation(
                                    d,
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

            if (settings.AppMode!=2)  // dont run if in batchmode
                new FileTool().SaveFileFromMap(ret);

            return ret;
        }
        private Node RandNode(Random rand, int width, int height , bool wall)
        {
            Node p = null;

            while (p == null || GridMap.Exists(i => i.Equals(p)))
            {
                int x = rand.Next(0, width);
                int y = rand.Next(0, height);
                p = new Node(x, y, !wall, DirectionMovement.None);
            }

            return p;
        }


    }
}