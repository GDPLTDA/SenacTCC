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
        public Coordinate(double tX, double tY)
        {
            X = tX;
            Y = tY;
        }
    }
}
