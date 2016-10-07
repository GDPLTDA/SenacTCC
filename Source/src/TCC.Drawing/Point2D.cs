﻿using OpenTK;

namespace TCC.Drawing
{
    public class Point2D
    {
        public Point2D(int tX, int tY, Color tColor)
        {
            X = tX;
            Y = tY;
            Color = tColor;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public Color Color { get; set; }
    }
}