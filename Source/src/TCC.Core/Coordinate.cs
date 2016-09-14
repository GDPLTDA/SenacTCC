using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TCC.Core
{
    public enum Direction
    {
        None,
        Up,
        Down,
        Left,
        Rigth
    }
    public class Coordinate
    {
        public Direction Dir { get; set; }
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

            Dir = tCoor.Dir;
        }
        public Coordinate(Direction tDir, double tX, double tY)
        {
            X = tX;
            Y = tY;

            Xi = Convert.ToInt32(tX);
            Yi = Convert.ToInt32(tY);

            Dir = tDir;
        }
    }
}
