using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TCC.Core
{
    public class Coordinate
    {
        public double X { get; set; }
        public double Y { get; set; }
        public int Xi { get; set; }
        public int Yi { get; set; }

        public Coordinate(Coordinate tCoor)
        {
            X = tCoor.X;
            Y = tCoor.Y;

            Xi = tCoor.Xi;
            Yi = tCoor.Yi;
        }

        public Coordinate(double tX, double tY)
        {
            X = tX;
            Y = tY;

            Xi = Convert.ToInt32(tX);
            Yi = Convert.ToInt32(tY);
        }
    }
}
