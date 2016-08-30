using System;
using System.Collections.Generic;

namespace TCC.GeneticAlgorithm
{
    public class GAGenome
    {
        private List<int> cities { get; set; }
        private double fitness { get; set; }
        public List<int> Cities
        {
            set { cities = value; }
            get { return cities; }
        }
        public double Fitness
        {
            set { fitness = value; }
            get { return fitness; }
        }
        private Random objRandom { get; set; }
        public GAGenome()
        {
            this.fitness = 0;
        }

        public GAGenome(int numberOfCities,Random objRandom)
        {
            this.fitness = 0;
            this.objRandom = objRandom;
            cities = GrabPermutation(numberOfCities);
        }
        private List<int> GrabPermutation(int limit)
        {
            var lstVecPerm = new List<int>();

            for (int i = 0; i < limit; i++)
            {
                var nextPossibleNumber = objRandom.Next(0, limit);

                while (TestNumber(lstVecPerm, nextPossibleNumber))
                {
                    nextPossibleNumber = objRandom.Next(0, limit);
                }

                lstVecPerm.Add(nextPossibleNumber);
            }
            return lstVecPerm;
        }
        private static bool TestNumber(List<int> vector, int NextPossibleNumber)
        {
            return vector.Contains(NextPossibleNumber);
        }
    }
}
