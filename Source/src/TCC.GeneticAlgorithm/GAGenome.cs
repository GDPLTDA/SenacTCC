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
        public GAGenome(SeachParameters tParams)
        {
            Fitness = 0;
            Route = RouteFinding(tParams);
        }

        public GAGenome(int numberOfRoute)
        {
            Fitness = 0;
            Route = GrabPermutation(numberOfRoute);
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
        /// <summary>
        /// Returns the eight locations immediately adjacent (orthogonally and diagonally) to <paramref name="fromLocation"/>
        /// </summary>
        /// <param name="fromLocation">The location from which to return all adjacent points</param>
        /// <returns>The locations as an IEnumerable of Points</returns>
        public static List<Coordinate> RouteFinding(SeachParameters tParam)
        {
            var lstVecPerm = new List<Coordinate>();
            var objRandom = new Random();
            bool run = true;
            var coor = tParam.LocationStart;

            while (run)
            {
                lstVecPerm.Add(coor);
                var lisadj = JJFunc.GetAdjacentLocations(coor);

                var i = objRandom.Next(0, lisadj.Count -1);

                if(!tParam.Valid(lisadj[i]))
                    run = false;

                coor = lisadj[i];
            }

            return lstVecPerm;
        }
    }
}
