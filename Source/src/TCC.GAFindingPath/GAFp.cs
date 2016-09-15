using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCC.Astar;
using TCC.GeneticAlgorithm;
using TCC.Core;

namespace TCC.GAFindingPath
{
    public class GAFP
    {
        Random objRandom = new Random();
        public int Generation { get; set; } = 0;
        int NumBest2Add { get; set; } = 1;
        List<GAGenome> ListPopulation { get; set; } = new List<GAGenome>();
        GAParams GaParams { get; set; }
        GAMapFP ObjMap { get; set; }
        int BestPopulation { get; set; }
        public double TotalFitness { get; set; }
        GAMutate ObjMutate { get; set; }
        GACrossOver ObjCrossOver { get; set; }
        public GAFP(GAParams tParams)
        {
            GaParams = tParams;
            ObjCrossOver = new GACrossOver(tParams, objRandom);
            ObjMutate = new GAMutate(tParams, objRandom);

            ObjMap = new GAMapFP(GaParams.Params);

            CreateStartingPopulation();
        }
        private void CreateStartingPopulation()
        {
            for (int i = 0; i < GaParams.PopulationSize; i++)
            {
                var objGenome = new GAGenome(GaParams.Params, objRandom);
                ListPopulation.Add(objGenome);
            }
        }
        public void Epoch()
        {
            CalculatePopulationFitness();

            var lstNewPop = new List<GAGenome>();

            ListPopulation = ListPopulation.OrderByDescending(x => x.Fitness).ToList();
            for (int i = 0; i < NumBest2Add; i++)
                lstNewPop.Add(new GAGenome(ListPopulation[i].Route, objRandom));

            while (lstNewPop.Count <= GaParams.PopulationSize)
            {
                var mom = RouletteWheelSelection();
                var dad = RouletteWheelSelection();

                var baby1List = new List<Coordinate>();
                var baby2List = new List<Coordinate>();

                ObjCrossOver.CrossoverPBX(mom.Route, dad.Route, out baby1List, out baby2List);

                baby1List = AdaptationBaby(baby1List);
                baby2List = AdaptationBaby(baby2List);

                //baby1List = ObjMutate.MutateIVM(baby1List);
                //baby2List = ObjMutate.MutateIVM(baby2List);

                var newcoor1 = GAGenome.AddCoor(GaParams.Params, baby1List.Last());
                var newcoor2 = GAGenome.AddCoor(GaParams.Params, baby2List.Last());

                //if (!baby1List.Exists(i=> i.Xi == newcoor1.Xi && i.Yi == newcoor1.Yi))
                    baby1List.Add(new Coordinate(newcoor1));
                //if (!baby2List.Exists(i => i.Xi == newcoor2.Xi && i.Yi == newcoor2.Yi))
                    baby2List.Add(new Coordinate(newcoor2));

                var baby1 = new GAGenome(baby1List, objRandom);
                var baby2 = new GAGenome(baby2List, objRandom);

                lstNewPop.Add(baby1);
                lstNewPop.Add(baby2);
            }
            ListPopulation = GA.CopyGenome(lstNewPop, objRandom);
            
            ++Generation;
        }
        public List<Coordinate> AdaptationBaby(List<Coordinate> tBaby)
        {
            tBaby = tBaby.Where(i => i.Xi != -1 && i.Yi != -1).ToList();

            var Search = GaParams.Params;

            var newbaby = new List<Coordinate>();
            newbaby.Add(tBaby[0]);

            for (int i = 1; i < tBaby.Count; i++)
            {
                var coor = JJFunc.CalcDir(newbaby.Last(), tBaby[i].Dir);

                if(Search.Valid(coor))
                    newbaby.Add(coor);
            }

            return newbaby;
        }

        private GAGenome RouletteWheelSelection()
        {
            var slice = objRandom.NextDouble() * TotalFitness;
            var total = (double)0;
            var selectedGenome = 0;

            for (int i = 0; i < GaParams.PopulationSize; i++)
            {
                total += ListPopulation[i].Fitness;

                if (total > slice)
                {
                    selectedGenome = i;
                    break;
                }
            }

            return new GAGenome(ListPopulation[selectedGenome].Route, objRandom);
        }
        void CalculatePopulationFitness()
        {
            TotalFitness = 0;
            var shortestRoute = double.MaxValue;
            var longestRoute = 0.0;

            for (int i = 0; i < GaParams.PopulationSize; ++i)
            {
                var tourLength = ObjMap.Get(ListPopulation[i].Route);
                ListPopulation[i].Fitness = tourLength;

                if (tourLength < shortestRoute)
                {
                    shortestRoute = tourLength;
                    BestPopulation = i;
                }
                if (tourLength > longestRoute)
                {
                    longestRoute = tourLength;
                }
            }

            for (int i = 0; i < GaParams.PopulationSize; ++i)
            {
                ListPopulation[i].Fitness = longestRoute - ListPopulation[i].Fitness;
                TotalFitness += ListPopulation[i].Fitness;
            }
        }
        public List<Coordinate> GetBestPath()
        {
            return ListPopulation[BestPopulation].Route.Where(i=>i!=null).ToList();
        }
        public double CalcFitness(List<Coordinate> tListCoor)
        {
            double fitness = 0;
            double teto = Math.Pow(GaParams.MapaSize, 2) * 3;



            return fitness;
        }
    }
}
