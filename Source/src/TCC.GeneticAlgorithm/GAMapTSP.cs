using System;
using System.Collections.Generic;
using TCC.Core;

namespace TCC.GeneticAlgorithm
{
    public class GAMapTSP
    {
        private int MapSize { get; set; }
        public List<Coordinate> CityCoordinates { get; set; }
        private int NumberOfCities { get; set; }
        private double BestRoute { get; set; }
        public GAMapTSP(int tNumberOfCities, int tMapSize)
        {
            MapSize = tMapSize;

            NumberOfCities = tNumberOfCities;
            CityCoordinates = new List<Coordinate>();

            CreateCitiesCirular(MapSize);
            CalculateBestPossibleRoute();
        }
        public void CreateCitiesCirular(double tMapSize)
        {
            var Ran = new Random();

            for (int i = 0; i < NumberOfCities; i++)
            {
                var x = Ran.Next(10, (int)tMapSize-50);
                var y = Ran.Next(10, (int)tMapSize-50);

                CityCoordinates.Add(new Coordinate(x,y));
            }
        }
        private static double CalculateA2B(Coordinate tA, Coordinate tB)
        {
            return Math.Sqrt(Math.Pow(tA.X - tB.X, 2) + Math.Pow(tA.Y - tB.Y, 2));
        }
        private void CalculateBestPossibleRoute()
        {
            BestRoute = 0;
            for (int i = 0; i < CityCoordinates.Count-1; i++)
            {
                BestRoute += CalculateA2B(CityCoordinates[i], CityCoordinates[i + 1]);
            }
            BestRoute += CalculateA2B(CityCoordinates[CityCoordinates.Count - 1],
                CityCoordinates[0]);
        }
        public double GetTourLength(List<int> tListOfCities)
        {
            double tourLength = 0;
                for (int i = 0; i < tListOfCities.Count - 1; i++)
                {
                    tourLength += CalculateA2B(CityCoordinates[tListOfCities[i]],
                        CityCoordinates[tListOfCities[i + 1]]);
                }
                tourLength += CalculateA2B(CityCoordinates[CityCoordinates.Count - 1],
                    CityCoordinates[0]);

            return tourLength;
        }
    }
}
