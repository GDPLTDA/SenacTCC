using System;
using System.Collections.Generic;
using TCC.Core;

namespace TCC.GeneticAlgorithm
{
    public class GAGenome
    {
        public List<Coordinate> Route { get; set; }
        public double Fitness { get; set; }
        public GAGenome()
        {
            Fitness = 0;
        }
        public GAGenome(int numberOfCities)
        {
            Fitness = 0;
            Route = GrabPermutation(numberOfCities);
        }
        private List<Coordinate> GrabPermutation(int limit)
        {
            var lstVecPerm = new List<Coordinate>();
            var objRandom = new Random();

            for (int i = 0; i < limit; i++)
            {
                var nextPossibleNumber = objRandom.Next(0, limit);
                
                while (TestNumber(lstVecPerm, nextPossibleNumber))
                    nextPossibleNumber = objRandom.Next(0, limit);

                lstVecPerm.Add(new Coordinate(nextPossibleNumber,0));
            }
            return lstVecPerm;
        }
        private static bool TestNumber(List<Coordinate> vector, int NextPossibleNumber)
        {
            return vector.Exists(i=>i.X == NextPossibleNumber);
        }
    }
}
