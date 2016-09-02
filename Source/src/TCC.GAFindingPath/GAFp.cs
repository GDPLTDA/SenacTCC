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
        public int generation { get; set; }
        int NumBest2Add { get; set; } = 1;
        private List<GAGenome> lstPopulation { get; set; } = new List<GAGenome>();
        private GAParams GaParams { get; set; }
        public SeachParameters SeachParams { get; set; }
        GAMapFP objMap;

        int BestPopulation;
        public double totalFitness;

        public GAFP(GAParams tParams, SeachParameters tSeachParams)
        {
            GaParams = tParams;
            SeachParams = tSeachParams;
            objMap = new GAMapFP(SeachParams);

            CreateStartingPopulation();
        }
        private void CreateStartingPopulation()
        {
            
            for (int i = 0; i < GaParams.PopulationSize; i++)
            {
                var objGenome = new GAGenome(SeachParams, objRandom);
                lstPopulation.Add(objGenome);
            }
        }
        public void Epoch()
        {
            CalculatePopulationFitness();

            var lstNewPop = new List<GAGenome>();

            lstPopulation = lstPopulation.OrderByDescending(x => x.Fitness).ToList();
            for (int i = 0; i < NumBest2Add; i++)
                lstNewPop.Add(lstPopulation[i]);

            while (lstNewPop.Count <= GaParams.PopulationSize)
            {
                var mom = RouletteWheelSelection();
                var dad = RouletteWheelSelection();

                var baby1List = new List<Coordinate>();
                var baby2List = new List<Coordinate>();

                CrossoverPBX(mom.Route.ToList(), dad.Route.ToList(), out baby1List, out baby2List);

                baby1List = MutateIVM(baby1List);
                baby2List = MutateIVM(baby2List);

                //var lBaby1 = baby2List.Last(i => i != null);
                //var coun1 = baby2List.Count(i => i != null);
                //var newcoor1 = GAGenome.AddCoor(SeachParams, lBaby1);

                //var lBaby2 = baby2List.Last(i => i != null);
                //var coun2 = baby2List.Count(i => i != null);
                //var newcoor2 = GAGenome.AddCoor(SeachParams, lBaby2);

                //if (coun1 < 1000)
                //    baby1List[coun1] = newcoor1;
                //if (coun2 < 1000)
                //    baby2List[coun2] = newcoor1;

                var baby1 = new GAGenome();
                baby1.Route = baby1List;
                var baby2 = new GAGenome();
                baby2.Route = baby2List;

                lstNewPop.Add(baby1);
                lstNewPop.Add(baby2);
            }
            lstPopulation = Copy(lstNewPop);
            
            ++generation;
        }

        public List<Coordinate> MutateIVM(List<Coordinate> vector)
        {
            if (objRandom.NextDouble() > GaParams.MutationRate)
                return vector;

            var lstMutated = JJFunc.Copy(vector);
            int beg, end;
            beg = end = 0;

            JJFunc.ChooseSection(out beg, out end, lstMutated.Count, 2);
            var lstTemp = new List<Coordinate>();
            for (int i = beg; i < end; i++)
            {
                lstTemp.Add(lstMutated[beg]);
                lstMutated.RemoveAt(beg);
            }
            lstTemp.Reverse();
            var count = 0;
            for (int i = beg; i < end; i++)
            {
                lstMutated.Insert(i, lstTemp[count]);
                count++;
            }
            return lstMutated;
        }
        private static List<GAGenome> Copy(List<GAGenome> listToCopy)
        {
            var lstReturn = new List<GAGenome>();
            for (int i = 0; i < listToCopy.Count; i++)
                lstReturn.Add(listToCopy[i]);

            return lstReturn;
        }

        private GAGenome RouletteWheelSelection()
        {

            var slice = objRandom.NextDouble() * totalFitness;
            var total = (double)0;
            var selectedGenome = 0;

            for (int i = 0; i < GaParams.PopulationSize; i++)
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

        private void CrossoverPBX(List<Coordinate> mum, List<Coordinate> dad, out List<Coordinate> baby1, out List<Coordinate> baby2)
        {
            baby1 = JJFunc.Copy(mum);
            baby2 = JJFunc.Copy(dad);

            if (objRandom.NextDouble() > GaParams.CrossoverRate || JJFunc.AreEqual(mum, dad))
                return;

            for (int i = 0; i < mum.Count; i++)
                baby1[i] = new Coordinate(-1, 0);
            for (int i = 0; i < dad.Count; i++)
                baby2[i] = new Coordinate(-1, 0);

            var lstPositions = new List<int>();
            var Pos = objRandom.Next(0, mum.Count - 1);

            while (Pos < mum.Count)
            {
                lstPositions.Add(Pos);
                Pos += objRandom.Next(1, mum.Count - Pos);
            }

            for (int pos = 0; pos < lstPositions.Count; ++pos)
            {
                baby1[lstPositions[pos]] = mum[lstPositions[pos]];
                baby2[lstPositions[pos]] = dad[lstPositions[pos]];
            }

            int c1, c2;
            c1 = c2 = 0;

            for (int pos = 0; pos < mum.Count; ++pos)
            {
                while (c2 < mum.Count && baby2[c2].X > -1)
                    ++c2;
                if (!baby2.Contains(mum[pos]))
                {
                    if (c2 < baby2.Count)
                        baby2[c2] = mum[pos];
                }

                while ((c1 < mum.Count) && (baby1[c1].X > -1))
                    ++c1;

                if (!baby1.Contains(dad[pos]))
                {
                    if(c1 < baby1.Count)
                        baby1[c1] = dad[pos];
                }
            }
        }

        private void CalculatePopulationFitness()
        {
            var shortestRoute = double.MaxValue;
            var longestRoute = 0.0;

            for (int i = 0; i < GaParams.PopulationSize; ++i)
            {
                var tourLength = objMap.Get(lstPopulation[i].Route.ToList());
                lstPopulation[i].Fitness = tourLength;

                if (tourLength < shortestRoute)
                {
                    shortestRoute = tourLength;
                    BestPopulation = i;
                }
                if (tourLength > longestRoute)
                    longestRoute = tourLength;
            }

            for (int i = 0; i < GaParams.PopulationSize; ++i)
            {
                lstPopulation[i].Fitness = longestRoute - lstPopulation[i].Fitness;
                totalFitness += lstPopulation[i].Fitness;
            }
        }

        public List<Coordinate> GetBestPath()
        {
            return lstPopulation[BestPopulation].Route.Where(i=>i!=null).ToList();
        }
    }
}
