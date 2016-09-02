using System;

namespace TCC.Core
{
    /// <summary>
    /// Defines the parameters which will be used to find a path across a section of the map
    /// </summary>
    public class SeachParameters
    {
        public Coordinate LocationStart { get; set; }
        public Coordinate LocationEnd { get; set; }
        public bool[,] Map { get; set; }
        public SeachParameters(Coordinate startLocation, Coordinate endLocation, bool[,] map)
        {
            LocationStart = startLocation;
            LocationEnd = endLocation;
            Map = map;
        }

        public bool Valid(Coordinate tCoor)
        {
            var ok = true;

            var x = Convert.ToInt32(tCoor.X);
            var y = Convert.ToInt32(tCoor.Y);
            var n = Map.GetLength(0);

            if (x >= n || x < 0)
                return false;

            n = Map.GetLength(1);
            if (y >= n || y < 0)
                return false;

            if (!Map[x, y])
                return false;
            
            var xf = Convert.ToInt32(LocationEnd.X);
            var yf = Convert.ToInt32(LocationEnd.Y);

            if (xf == x && yf == x)
                ok = false;

            return ok;
        }
    }
}
