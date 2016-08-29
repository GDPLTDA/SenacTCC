using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCC.Core;

namespace TCC.AStar
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
    }
}
