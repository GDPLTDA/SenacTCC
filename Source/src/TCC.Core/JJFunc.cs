using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TCC.Core
{
    public static class JJFunc
    {
        public static double CalcteA2B(Coordinate tA, Coordinate tB)
        {
            return Math.Sqrt(Math.Pow(tA.X - tB.X, 2) + Math.Pow(tA.Y - tB.Y, 2));
        }

        public static bool[,] GetMap()
        {
            int w = 30, h = 20;
            var map = new bool[w, h];
            for (int y = 0; y < h; y++)
                for (int x = 0; x < w; x++)
                    map[x, y] = true;

            return map;
        }

        public static List<Coordinate> GetAdjacentLocations(Coordinate fromLocation)
        {
            return new List<Coordinate>
            {
                new Coordinate(fromLocation.X-1, fromLocation.Y-1),
                new Coordinate(fromLocation.X-1, fromLocation.Y  ),
                new Coordinate(fromLocation.X-1, fromLocation.Y+1),
                new Coordinate(fromLocation.X,   fromLocation.Y+1),
                new Coordinate(fromLocation.X+1, fromLocation.Y+1),
                new Coordinate(fromLocation.X+1, fromLocation.Y  ),
                new Coordinate(fromLocation.X+1, fromLocation.Y-1),
                new Coordinate(fromLocation.X,   fromLocation.Y-1)
            };
        }
    }
}
