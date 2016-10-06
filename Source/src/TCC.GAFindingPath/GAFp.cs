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
        int NumBest2Add { get; set; } = 10;
        List<GAGenome> ListPopulation { get; set; } = new List<GAGenome>();
        GAParams GaParams { get; set; }
        int BestPopulation { get; set; }
        public double TotalFitness { get; set; }
        GACrossOver ObjCrossOver { get; set; }
        public GAFP(GAParams tParams)
        {
            GaParams = tParams;
            ObjCrossOver = new GACrossOver(tParams, objRandom);

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

                baby1List = MutationBaby(baby1List);
                baby2List = MutationBaby(baby2List);
                
                var baby1 = new GAGenome(baby1List, objRandom);
                var baby2 = new GAGenome(baby2List, objRandom);

                lstNewPop.Add(baby1);
                lstNewPop.Add(baby2);
            }
            ListPopulation = lstNewPop;
            
            Generation++;
        }
        public List<Coordinate> MutationBaby(List<Coordinate> tBaby)
        {
            tBaby = AdaptationBaby(tBaby);

            if (GaParams.Params.LocEnd.Equals(tBaby.Last()))
                return tBaby;

            var newcoor = GAGenome.AddCoor(GaParams.Params, tBaby);

            if (!tBaby.Exists(i => i.Equals(newcoor)))
                tBaby.Add(new Coordinate(newcoor));

            return tBaby;
        }
        public List<Coordinate> AdaptationBaby(List<Coordinate> tBaby)
        {
            tBaby = tBaby.Where(i => !i.Equals(-1, -1)).ToList();

            var Search = GaParams.Params;

            var newbaby = new List<Coordinate>();
            newbaby.Add(tBaby[0]);

            for (int i = 1; i < tBaby.Count; i++)
            {
                var coor = new Coordinate(newbaby.Last(), tBaby[i].DirCoor);

                if(Search.Valid(coor) && !newbaby.Exists(x => x.Equals(coor)))
                    newbaby.Add(coor);

                if (coor.Equals(Search.LocEnd))
                    break;
            }

            return newbaby;
        }

        private GAGenome RouletteWheelSelection()
        {
            var slice = objRandom.NextDouble() * TotalFitness;
            var total = (double)0;
            var selectedGenome = 0;// objRandom.Next(0, ListPopulation.Count - 1);

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

            for (int i = 0; i < GaParams.PopulationSize; ++i)
                ListPopulation[i].Fitness = CalcFitness(ListPopulation[i].Route);

            var coorend = GaParams.Params.LocEnd;

            //ListPopulation = ListPopulation.OrderBy(i => JJFunc.CalcteA2B(i.Route.Last(), coorend)).ToList();
            ListPopulation = ListPopulation.OrderBy(i => i.Fitness).ToList();


            var shortestRoute = ListPopulation.Min(i => i.Fitness) ;
            var longestRoute = ListPopulation.Max(i => i.Fitness);
            BestPopulation = 0; // ListPopulation.Count -1 ;

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
            double teto = (GaParams.MapWidth+GaParams.MapHeight)*3;
            
            var Gx = GaParams.Params.LocEnd.X;
            var Gy = GaParams.Params.LocEnd.Y;
            
            var Sx = tListCoor.Last().X;
            var Sy = tListCoor.Last().Y;

            var horizontal = tListCoor.Sum(e => {
                if ( new Direction[]{ Direction.None, Direction.Up, Direction.Down}.Contains(e.DirCoor) )
                    return 0;

                return e.DirCoor == Direction.Left ? -1 : 1;
            });


            var vertical = tListCoor.Sum(e => {
                if ( new Direction[]{ Direction.None, Direction.Left, Direction.Rigth}.Contains(e.DirCoor) )
                    return 0;

                return e.DirCoor == Direction.Down ? -1 : 1;
            });

            var MH = Math.Abs(Gx-(Sx+horizontal) * GaParams.MapWidth ) + 
                     Math.Abs(Gy-(Sy+vertical) * GaParams.MapHeight);
            
            var MT = horizontal*GaParams.MapWidth + vertical*GaParams.MapHeight;

            fitness = teto - MH - MT * 0.1f;

            return fitness;
        }
    }
}
