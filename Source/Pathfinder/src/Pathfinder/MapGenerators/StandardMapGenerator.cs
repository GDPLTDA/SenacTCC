using Pathfinder.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Pathfinder.MapGenerators
{
    public class StandardMapGenerator : IMapGenerator
    {
        public List<Node> GridMap { get; set; } = new List<Node>();
        public IMap DefineMap(string argument, DiagonalMovement? diagonal = null)
        {
            var width = Settings.Width;
            var height = Settings.Height;
            var seed = Settings.RandomSeed;
            var minPathLength = Settings.MinimumPath;
            var blocksize = Settings.RandomBlock;
            var IsAGoodMap = false;
            IMap ret = null;
            if (argument != string.Empty)
            {
                var param = argument.Split('|');
                try
                {
                    width = int.Parse(param[0]);
                    height = int.Parse(param[1]);
                    seed = double.Parse(param[2]) / 100;
                    minPathLength = int.Parse(param[3]);
                }
                catch
                {
                    throw new Exception("Ivalid random map parameters!");
                }
            }
            var d = Settings.AllowDiagonal;
            if (diagonal.HasValue)
                d = diagonal.Value;
            // finder para valida se o mapa é passavel
            var AStar = Container.Resolve<IFinder>((int)FinderEnum.AStar);

            AStar.DiagonalMovement = d;
            AStar.Heuristic = Container.Resolve<IHeuristic>((int)HeuristicEnum.Octile);
            var subgrid = new List<Node>();
            while (!IsAGoodMap)
            {
                var nodes = new List<Node>();
                var _map = new Map(width, height)
                {
                    AllowDiagonal = d
                };

                var size = Convert.ToInt32(blocksize * blocksize * seed);
                var rand = new Random();
                while (size > 0)
                {
                    var p = RandNode(rand, blocksize, blocksize, true);
                    subgrid.Add(p);
                    size--;
                }
                for (int i = 0; i < width; i += blocksize)
                {
                    for (int j = 0; j < height; j += blocksize)
                    {
                        foreach (var item in subgrid)
                        {
                            var x = item.X + i;
                            var y = item.Y + j;
                            if (x < width && y < height)
                            {
                                var node = new Node(x, y, false);
                                GridMap.Add(node);
                            }
                        }
                    }
                }
                _map.DefineAllNodes(GridMap);
                _map.StartNode = RandNode(rand, width, height, false);
                _map.EndNode = RandNode(rand, width, height, false);
                if (!_map.ValidMap())
                    throw new Exception("Invalid map configuration");
                if (AStar.Find(_map)) // verifica se o mapa possui um caminho
                {
                    var path = AStar.GetPath();
                    if (path.Max(e => e.G) >= minPathLength) // verifica se o caminho sastifaz o tamanho minimo
                    {
                        IsAGoodMap = true;
                        _map.Clear();
                        ret = _map;
                    }
                }
                GridMap = new List<Node>();
                subgrid = new List<Node>();
            }
            if (Settings.AutoSaveMaps)  // dont run if in batchmode
                FileTool.SaveFileFromMap(ret);
            return ret;
        }
        private Node RandNode(Random rand, int width, int height, bool wall)
        {
            Node p = null;
            while (p == null || GridMap.Exists(i => i.Equals(p)))
            {
                var x = rand.Next(0, width);
                var y = rand.Next(0, height);
                p = new Node(x, y, !wall, DirectionMovement.None);
            }
            return p;
        }
    }
}