using System;
using System.Collections.Generic;
using TCC.Core;

namespace TCC.GeneticAlgorithm
{
    public class GAMapTSP
    {
        public List<Coordinate> Cities { get; set; }
        private int NumberOfRoutes { get; set; }
        private double BestRoute { get; set; }

        public GAMapTSP(int tNumberOfCities, int tMapSize)
        {
            NumberOfRoutes = tNumberOfCities;
            Cities = new List<Coordinate>();

            CreateCitiesRandom(tMapSize);
            CalculateBestPossibleRoute();
        }
        public void CreateCitiesRandom(double tMapSize)
        {
            var Ran = new Random();
            var size = (int)tMapSize - 50;

            for (int i = 0; i < NumberOfRoutes; i++)
            {
                var x = Ran.Next(10, size);
                var y = Ran.Next(10, size);

                Cities.Add(new Coordinate(x,y));
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

            for (int i = 0; i < tListOfCities.Count - 1; i++) {
                var a = Convert.ToInt32(tListOfCities[i].X);
                var b = Convert.ToInt32(tListOfCities[i + 1].X);

                tourLength += JJFunc.CalcteA2B(Cities[a], Cities[b]);
            }

            tourLength += JJFunc.CalcteA2B(Cities[Cities.Count - 1],
                Cities[0]);

            return tourLength;
        }
    }
}
