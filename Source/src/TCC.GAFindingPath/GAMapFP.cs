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

        public GAMapFP(SeachParameters tConfig)
        {
            Config = tConfig;
        }
        public double Get(List<Coordinate> tListRoute)
        {
            double tourLength = 0;

            for (int i = 0; i < tListRoute.Count - 1; i++)
                tourLength += JJFunc.CalcteA2B(tListRoute[i], tListRoute[i + 1]) / 5;

            tourLength += 1000 * JJFunc.CalcteA2B(tListRoute.Last(), Config.LocationEnd);

            return tourLength;
        }

    }
}
