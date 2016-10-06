using System;

namespace TCC.Core
{
    /// <summary>
    /// Defines the parameters which will be used to find a path across a section of the map
    /// </summary>
    public class SeachParameters
    {
        public Coordinate LocStart { get; set; }
        public Coordinate LocEnd { get; set; }
        public bool[,] Map { get; set; }
        public SeachParameters(Coordinate startLocation, Coordinate endLocation, bool[,] map)
        {
            LocStart = startLocation;
            LocEnd = endLocation;
            Map = map;
        }

        public bool Valid(Coordinate tCoor)
        {
            var ok = true;

            var x = tCoor.Xi;
            var y = tCoor.Yi;
            var n = Map.GetLength(0);

            if (x >= n || x < 0)
                return false;

            n = Map.GetLength(1);
            if (y >= n || y < 0)
                return false;

            if (!Map[x, y])
                return false;
            return ok;
        }
    }
}
