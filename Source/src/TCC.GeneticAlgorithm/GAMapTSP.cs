using System;
using System.Collections.Generic;
using TCC.Core;

namespace TCC.GeneticAlgorithm
{
    public class GAMapTSP
    {
        private int MapSize;
        public List<Coordinate> lstCityCoordinates;
        public List<Coordinate> CityCoordinates
        {
            get { return lstCityCoordinates; }
        }
        private int numberOfCities;
        private double bestPossibleRoute;
        public double BestPossibleRoute
        {
            get { return bestPossibleRoute; }
        }

        public GAMapTSP(int tNumberOfCities,int tMapSize)
        {
            MapSize = tMapSize;

            numberOfCities = tNumberOfCities;
            lstCityCoordinates = new List<Coordinate>();

            CreateCitiesCirular(MapSize);
            CalculateBestPossibleRoute();
        }

        
        public void CreateCitiesCirular(double MapSize)
        {
            var Ran = new Random();

            for (int i = 0; i < numberOfCities; i++)
            {
                var x = Ran.Next(10,(int)MapSize-50);
                var y = Ran.Next(10, (int)MapSize-50);

                lstCityCoordinates.Add(new Coordinate(x,y));
            }
        }

        private static double CalculateA2B(Coordinate A, Coordinate B)
        {
            return Math.Sqrt(Math.Pow(A.X - B.X, 2) + Math.Pow(A.Y - B.Y, 2));
        }

        private void CalculateBestPossibleRoute()
        {
            bestPossibleRoute = 0;
            for (int i = 0; i < lstCityCoordinates.Count-1; i++)
            {
                bestPossibleRoute += CalculateA2B(lstCityCoordinates[i], lstCityCoordinates[i + 1]);
            }
            bestPossibleRoute += CalculateA2B(lstCityCoordinates[lstCityCoordinates.Count - 1],
                lstCityCoordinates[0]);
        }

        public double GetTourLength(List<int> listOfCities)
        {
            double tourLength = 0;
                for (int i = 0; i < listOfCities.Count - 1; i++)
                {
                    tourLength += CalculateA2B(lstCityCoordinates[listOfCities[i]],
                        lstCityCoordinates[listOfCities[i + 1]]);
                }
                tourLength += CalculateA2B(lstCityCoordinates[lstCityCoordinates.Count - 1],
                    lstCityCoordinates[0]);

            return tourLength;
        }
    }
}
