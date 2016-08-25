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
    public class ASSearchParameters
    {
        public Coordinate StartLocation { get; set; }

        public Coordinate EndLocation { get; set; }
        
        public bool[,] Map { get; set; }

        public ASSearchParameters(Coordinate startLocation, Coordinate endLocation, bool[,] map)
        {
            StartLocation = startLocation;
            EndLocation = endLocation;
            Map = map;
        }
    }
}
