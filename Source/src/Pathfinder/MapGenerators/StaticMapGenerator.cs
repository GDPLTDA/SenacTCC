using Pathfinder.Abstraction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
namespace Pathfinder.MapGenerators
{
    public class StaticMapGenerator : IMapGenerator
    {
        public IMap DefineMap(string argument, DiagonalMovement? diagonal = null)
        {
            const int width = 7;
            const int height = 5;
            var nodes = new List<Node>();
            var ret = new Map(width, height);
            if (argument=="")
            {
                var choose = 0;
                while (choose < 49 || choose >51)
                {
                    Console.Clear();
                    Console.Write("Choose the map: \n 1=Without Wall\n 2=Wall with gap\n 3=Wall without gap\n=>");
                    choose = Console.Read();
                }
                var opt = new Dictionary<int, string>
                {
                    [49] = "NoWall",
                    [50] = "WithGap",
                    [51] = "WithoutGap"
                };
                argument = opt[choose];
            }
            switch (argument)
            {
                case "NoWall":
                    WallSimple(ret);
                    break;
                case "WithGap":
                    WallWithGap(ret);
                    break;
                case "WithoutGap":
                    WallWithoutGap(ret);
                    break;
                default:
                    break;
            }
            if (!ret.ValidMap())
                throw new Exception("Invalid map configuration");
            return ret;
        }
        /// <summary>
        /// Creates a clear map with a start and end point and sets up the search parameters
        /// </summary>
        /// <param name="map">
        private static void WallSimple(IMap map)
        {
            //  □ □ □ □ □ □ □
            //  □ □ □ □ □ □ □
            //  □ F □ □ □ S □
            //  □ □ □ □ □ □ □
            //  □ □ □ □ □ □ □
            map.StartNode = map[2, 1];
            map.EndNode =  map[2, 5];
        }
        /// <summary>
        /// Create an L-shaped wall between S and F
        /// </summary>
        /// <param name="map">
        private static void WallWithGap(IMap map)
        {
            //  □ □ □ ■ □ □ □
            //  □ □ □ ■ □ □ □
            //  □ S □ ■ □ F □
            //  □ □ □ ■ ■ □ □
            //  □ □ □ □ □ □ □
            // Path: 1,2 ; 2,1 ; 3,0 ; 4,0 ; 5,1 ; 5,2
            map[0, 3].Walkable = false;
            map[1, 3].Walkable = false;
            map[2, 3].Walkable = false;
            map[3, 3].Walkable = false;
            map[3, 4].Walkable = false;
            map.StartNode = map[2, 1];
            map.EndNode = map[2, 5];
        }
        /// <summary>
        /// Create a closed barrier between S and F
        /// </summary>
        /// <param name="map">
        private static void WallWithoutGap(IMap map)
        {
            //  □ □ □ ■ □ □ □
            //  □ □ □ ■ □ □ □
            //  □ S □ ■ □ F □
            //  □ □ □ ■ □ □ □
            //  □ □ □ ■ □ □ □
            // No path
            map[0, 3].Walkable = false;
            map[1, 3 ].Walkable = false;
            map[2, 3 ].Walkable = false;
            map[3, 3 ].Walkable = false;
            map[4, 3 ].Walkable = false;
            map.StartNode = map[2, 1];
            map.EndNode = map[2, 5];
        }
    }
}