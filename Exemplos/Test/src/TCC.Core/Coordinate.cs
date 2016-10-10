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
        Rigth,
        UpLeft,
        UpRigth,
        DownLeft,
        DownRigth
    }
    public class Coordinate
    {
        public Direction DirCoor { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public int Xi { get; set; }
        public int Yi { get; set; }

        public bool Equals(int x, int y)
        {
            return X == x && Y == y;
        }
        public bool Equals(Coordinate obj)
        {
            return X == obj.X && Y == obj.Y;
        }
        public Coordinate(Coordinate tCoor)
        {
            X = tCoor.X;
            Y = tCoor.Y;

            Xi = tCoor.Xi;
            Yi = tCoor.Yi;

            DirCoor = tCoor.DirCoor;
        }
        public Coordinate(Coordinate tCoor, Direction tDir)
        {
            X = tCoor.X;
            Y = tCoor.Y;

            switch (tDir)
            {
                case Direction.Up:
                    Y += -1;
                    break;
                case Direction.Down:
                    Y += 1;
                    break;
                case Direction.Left:
                    X += -1;
                    break;
                case Direction.Rigth:
                    X += 1;
                    break;

                case Direction.UpLeft:
                    X += -1;
                    Y += -1;
                    break;
                case Direction.DownLeft:
                    X += -1;
                    Y += 1;
                    break;
                case Direction.UpRigth:
                    X += 1;
                    Y += -1;
                    break;
                case Direction.DownRigth:
                    X += 1;
                    Y += 1;
                    break;
                default:
                    break;
            }
            DirCoor = tDir;
            Xi = Convert.ToInt32(X);
            Yi = Convert.ToInt32(Y);
        }

        public Coordinate(double tX, double tY)
        {
            X = tX;
            Y = tY;

            Xi = Convert.ToInt32(tX);
            Yi = Convert.ToInt32(tY);

            DirCoor = Direction.None;
        }

        public Coordinate(Direction tDir, double tX, double tY)
        {
            X = tX;
            Y = tY;

            Xi = Convert.ToInt32(tX);
            Yi = Convert.ToInt32(tY);

            DirCoor = tDir;
        }
    }
}
