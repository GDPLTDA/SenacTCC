using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace xnafan.MazeCreator
{
    public class PointEventArgs : EventArgs
    {
        public Point Point  { get; private set; }

        public PointEventArgs(Point point)
        {
            this.Point = point;
        }
    }
}
