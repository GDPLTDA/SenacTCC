using System;
using System.Collections.Generic;
using TCC.Core;

namespace TCC.GAPathShort
{
    public class GATSPMap
    {
        public List<Coordinate> Cities { get; set; }
        private int NumberOfRoutes { get; set; }
        public double BestRoute { get; set; }

        public GATSPMap(int tNumberOfCities, int MapWidth, int MapHeight)
        {
            NumberOfRoutes = tNumberOfCities;
            Cities = new List<Coordinate>();

            CreateCitiesRandom(MapWidth, MapHeight);
            CalculateBestPossibleRoute();
        }
        public void CreateCitiesRandom(int MapWidth, int MapHeight)
        {
            var Ran = new Random();
            var sizeW = MapWidth - 50;
            var sizeH = MapHeight - 50;

            for (int i = 0; i < NumberOfRoutes; i++)
            {
                var x = Ran.Next(10, sizeW);
                var y = Ran.Next(10, sizeH);

                Cities.Add(new Coordinate(0, x, y));
            }
        }
        private void CalculateBestPossibleRoute()
        {
            BestRoute = 0;
            for (int i = 0; i < Cities.Count-1; i++)
                BestRoute += JJFunc.CalcteA2B(Cities[i], Cities[i + 1]);

            BestRoute += JJFunc.CalcteA2B(Cities[Cities.Count - 1],
                Cities[0]);
        }
        public double GetTourLength(List<Coordinate> tListOfCities)
        {
            double tourLength = 0;

            for (int i = 0; i < tListOfCities.Count - 1; i++) 
                tourLength += JJFunc.CalcteA2B(Cities[tListOfCities[i].Xi], Cities[tListOfCities[i + 1].Xi]);

            tourLength += JJFunc.CalcteA2B(Cities[Cities.Count - 1],
                Cities[0]);

            return tourLength;
        }
    }
}
