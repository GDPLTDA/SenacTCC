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
    }
}
