using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TCC.Core
{
    public static class JJFunc
    {
        public static List<Coordinate> EqualSize(List<Coordinate> tA, List<Coordinate> tB)
        {
            int ca = tA.Count, cb = tB.Count;

            if(ca < cb)
                for (int i = 0; i < cb - ca; i++)
                    tA.Add(new Coordinate(-1, -1));

            return tA;
        }

        public static double CalcteA2B(Coordinate tA, Coordinate tB)
        {
            return Math.Sqrt(Math.Pow(tA.X - tB.X, 2) + Math.Pow(tA.Y - tB.Y, 2));
        }
        public static bool[,] GetMap(int w = 30,int h = 20)
        {
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

        public static List<Coordinate> Copy(List<Coordinate> listToCopy)
        {
            var lstReturn = new List<Coordinate>();
            for (int i = 0; i < listToCopy.Count; i++)
                lstReturn.Add(new Coordinate(listToCopy[i]));

            return lstReturn;
        }

        public static bool AreEqual(List<Coordinate> lst1, List<Coordinate> lst2)
        {
            if (lst1.Count != lst2.Count)
                return false;

            for (int i = 0; i < lst1.Count; i++)
                if (lst1[i] != lst2[i])
                    return false;

            return true;
        }

        public static void ChooseSection(out int beg, out int end, int maxSpanSize, int minSpanSize)
        {
            var objRandom = new Random();
            var spanSize = objRandom.Next(minSpanSize, maxSpanSize);
            beg = objRandom.Next(0, maxSpanSize - spanSize);
            end = beg + spanSize;
        }
    }
}
