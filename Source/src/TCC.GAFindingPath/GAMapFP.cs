using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCC.Astar;
using TCC.Core;

namespace TCC.GAFindingPath
{
    public class GAMapFP
    {
        SeachParameters Config { get; set; }
        public List<Coordinate> Positions { get; set; }

        public GAMapFP(SeachParameters tConfig)
        {
            Config = tConfig;
        }


    }
}
