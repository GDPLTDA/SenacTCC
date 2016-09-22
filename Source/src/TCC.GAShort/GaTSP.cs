using System;
using System.Collections.Generic;
using System.Linq;
using TCC.Core;
using TCC.GeneticAlgorithm;

namespace TCC.GAPathShort
{
    public class GATSP
    {
        private List<GAGenome> lstPopulation { get; set; } = new List<GAGenome>();
        private GATSPMap objMap { get; set; }
        private GAParams Params { get; set; }
        private int NumBest2Add { get; set; } = 1;
        private double totalFitness { get; set; }
        private double shortestRoute { get; set; } = double.MaxValue;
        private double longestRoute { get; set; } = 0;
        private int fittestGenome { get; set; } = 0;
        public int generation { get; set; } = 0;
        public double BestSolution { get { return objMap.BestRoute; } } 
        private Random objRandom { get; set; } = new Random();
        GAMutate ObjMutate { get; set; }
        GACrossOver ObjCrossOver { get; set; }
        public GATSP(GAParams tParams)
        {
            Params = tParams;
            ObjCrossOver = new GACrossOver(tParams, objRandom);
            ObjMutate = new GAMutate(tParams, objRandom);

            objMap = new GATSPMap(tParams.NumberOfRoutes, tParams.MapWidth, tParams.MapHeight);
            CreateStartingPopulation();
        }
        public List<Coordinate> GetlstCityCoordinates()
        {
            return objMap.Cities;
        }

        private GAGenome RouletteWheelSelection()
        {
            var slice = objRandom.NextDouble() * totalFitness;
            var total = (double)0;
            var selectedGenome = 0;

            for (int i = 0; i < Params.PopulationSize; i++)
            {
                total += lstPopulation[i].Fitness;

                if (total > slice)
                {
                    selectedGenome = i;
                    break;
                }
            }
            return lstPopulation[selectedGenome];
        }
        private void CalculatePopulationFitness()
        {
            totalFitness = 0;
            for (int i = 0; i < Params.PopulationSize; ++i)
            {
                var tourLength = objMap.GetTourLength(lstPopulation[i].Route);
                lstPopulation[i].Fitness = tourLength;

                if (tourLength < shortestRoute)
                {
                    shortestRoute = tourLength;
                    fittestGenome = i;
                }
                if (tourLength > longestRoute)
                    longestRoute = tourLength;
            }

            for (int i = 0; i < Params.PopulationSize; ++i)
            {
                lstPopulation[i].Fitness = longestRoute - lstPopulation[i].Fitness;
                totalFitness += lstPopulation[i].Fitness;
            }
        }

        public List<Coordinate> GetBestCitie()
        {
            return lstPopulation[fittestGenome].Route;
        }
        public List<Coordinate> GetCityCoordinates()
        {
            return objMap.Cities;
        }

        public void Epoch()
        {
            Reset();

            CalculatePopulationFitness();

            var lstNewPop = new List<GAGenome>();

            lstPopulation = lstPopulation.OrderByDescending(x => x.Fitness).ToList();
            for (int i = 0; i < NumBest2Add; i++)
                lstNewPop.Add(new GAGenome(lstPopulation[i].Route, objRandom));

            while (lstNewPop.Count <= Params.PopulationSize)
            {
                var mom = RouletteWheelSelection();
                var dad = RouletteWheelSelection();

                var baby1List = new List<Coordinate>();
                var baby2List = new List<Coordinate>();

                ObjCrossOver.CrossoverPBX(mom.Route, dad.Route, out baby1List, out baby2List);

                baby1List = ObjMutate.MutateIVM(baby1List);
                baby2List = ObjMutate.MutateIVM(baby2List);

                var baby1 = new GAGenome(baby1List, objRandom);
                var baby2 = new GAGenome(baby2List, objRandom);

                lstNewPop.Add(baby1);
                lstNewPop.Add(baby2);
            }
            lstPopulation = lstNewPop;

            ++generation;
        }
        private void CreateStartingPopulation()
        {
            for (int i = 0; i < Params.PopulationSize; i++)
            {
                var objGenome = new GAGenome(Params.NumberOfRoutes, objRandom);
                lstPopulation.Add(objGenome);
            }
        }
        private void Reset()
        {
            fittestGenome = 0;
            shortestRoute = double.MaxValue;
            longestRoute = 0;
        }
    }
}
