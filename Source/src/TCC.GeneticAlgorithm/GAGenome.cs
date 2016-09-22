using System;
using System.Collections.Generic;
using System.Threading;
using TCC.Core;
using System.Linq;

namespace TCC.GeneticAlgorithm
{
    public class GAGenome
    {
        public List<Coordinate> Route { get; set; }
        public double Fitness { get; set; }
        static Random objRandom;

        public GAGenome(List<Coordinate> tListRoute, Random tobjRandom)
        {
            objRandom = tobjRandom;
            Route = tListRoute.Select(i=>new Coordinate(i)).ToList();
            Fitness = 0;
        }
        public GAGenome(SeachParameters tParams, Random tobjRandom)
        {
            objRandom = tobjRandom;
            Fitness = 0;
            Route = RouteFinding(tParams);
        }

        public GAGenome(int numberOfRoute, Random tobjRandom)
        {
            objRandom = tobjRandom;
            Fitness = 0;
            Route = GrabPermutation(numberOfRoute);
        }
        private List<Coordinate> GrabPermutation(int limit)
        {
            var lstVecPerm = new List<Coordinate>();

            for (int i = 0; i < limit; i++)
            {
                var nextPossibleNumber = objRandom.Next(0, limit);
                
                while (TestNumber(lstVecPerm, nextPossibleNumber))
                    nextPossibleNumber = objRandom.Next(0, limit);

                lstVecPerm.Add(new Coordinate(0, nextPossibleNumber, 0));
            }
            return lstVecPerm;
        }
        private bool TestNumber(List<Coordinate> vector, int NextPossibleNumber)
        {
            return vector.Exists(i=>i.Xi == NextPossibleNumber);
        }
        /// <summary>
        /// Returns the eight locations immediately adjacent (orthogonally and diagonally) to <paramref name="fromLocation"/>
        /// </summary>
        /// <param name="fromLocation">The location from which to return all adjacent points</param>
        /// <returns>The locations as an IEnumerable of Points</returns>
        public List<Coordinate> RouteFinding(SeachParameters tParam)
        {
            var lstVecPerm = new List<Coordinate>();
            bool run = true;
            var coor = tParam.LocStart;

            while (run)
            {
                // verfica se não está voltando para o mesmo no anterior
                if(!lstVecPerm.Exists(x=>x.Xi == coor.Xi && x.Yi == coor.Yi))
                    lstVecPerm.Add(coor);


                var dir = objRandom.Next(1, Enum.GetNames(typeof(Direction)).Length);
                var coordir = new Coordinate(coor, (Direction)dir);

                // verifica se teve colisão ou se encontrou o fim
                run = tParam.Valid(coordir);

                coor = coordir;
            }

            return lstVecPerm;
        }

        public static Coordinate AddCoor(SeachParameters tParam, Coordinate tCoor)
        {
            var run = true;
            var i = 0;
            Coordinate coordir = tCoor;
            while (run)
            {
                var dir = objRandom.Next(1, Enum.GetNames(typeof(Direction)).Length);
                coordir = new Coordinate(tCoor, (Direction)dir);

                run = !tParam.Valid(coordir);
            }
            return coordir;
        }
    }
}
