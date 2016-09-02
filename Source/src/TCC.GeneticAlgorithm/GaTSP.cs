using System;
using System.Collections.Generic;
using System.Linq;
using TCC.Core;

namespace TCC.GeneticAlgorithm
{
    public class GaTSP
    {
        private List<GAGenome> lstPopulation { get; set; } = new List<GAGenome>();
        private GAMapTSP objMap { get; set; }
        private GAParams Params { get; set; }
        private int NumBest2Add { get; set; } = 1;
        private double totalFitness { get; set; }
        private double shortestRoute { get; set; } = double.MaxValue;
        private double longestRoute { get; set; } = 0;
        private int fittestGenome { get; set; } = 0;
        public int generation { get; set; } = 0;
        public double BestSolution { get { return objMap.BestRoute; } } 
        private Random objRandom { get; set; } = new Random();
        public GaTSP(GAParams tParams)
        {
            Params = tParams;

            objMap = new GAMapTSP(tParams.NumberOfRoutes, tParams.MapaSize);
            CreateStartingPopulation();
        }
        public List<Coordinate> GetlstCityCoordinates()
        {
            return objMap.Cities;
        }
        private List<Coordinate> Mutation(List<Coordinate> vector)
        {
            var lstMutated = JJFunc.Copy(vector);

            if (objRandom.NextDouble() > Params.MutationRate) return lstMutated;

            var pos1 = objRandom.Next(0, Params.NumberOfRoutes);
            var pos2 = pos1;

            while (pos1 == pos2)
                pos2 = objRandom.Next(0, Params.NumberOfRoutes);

            var temp = lstMutated[pos1];
            lstMutated[pos1] = lstMutated[pos2];
            lstMutated[pos2] = temp;

            return lstMutated;
        }
        public List<Coordinate> MutateSM(List<Coordinate> vector)
        {
            var lstMutated = JJFunc.Copy(vector);
            if (objRandom.NextDouble() > Params.MutationRate) return lstMutated;

            const int minSpanSize = 3;
            int beg, end;
            beg = end = 0;
            JJFunc.ChooseSection(out beg, out end, lstMutated.Count, minSpanSize);

            var span = end - beg;
            var numberOfSwaprsRequired = span;

            while (numberOfSwaprsRequired != 0)
            {
                var no1 = objRandom.Next(beg, end + 1);
                var no2 = objRandom.Next(beg, end + 1);

                var temp = lstMutated[no1];
                lstMutated[no1] = lstMutated[no2];
                lstMutated[no2] = temp;

                --numberOfSwaprsRequired;
            }

            return lstMutated;
        }
        
        public List<Coordinate> MutateDM(List<Coordinate> vector)
        {
            var lstMutated = JJFunc.Copy(vector);
            if (objRandom.NextDouble() > Params.MutationRate) return lstMutated;

            const int minSpanSize = 3;
            int beg, end;
            beg = end = 0;
            JJFunc.ChooseSection(out beg, out end, lstMutated.Count, minSpanSize);

            var lstTemp = new List<Coordinate>();
            for (int i = beg; i < end; i++)
            {
                lstTemp.Add(lstMutated[beg]);
                lstMutated.RemoveAt(beg);
            }

            var insertLocation = objRandom.Next(0, lstMutated.Count + 1);
            var count = 0;
            for (int i = insertLocation; count < lstTemp.Count; i++)
            {
                lstMutated.Insert(i, lstTemp[count]);
                count++;
            }

            return lstMutated;
        }
        public List<Coordinate> MutateIM(List<Coordinate> vector)
        {
            var lstMutated = JJFunc.Copy(vector);
            if (objRandom.NextDouble() > Params.MutationRate) return lstMutated;

            var randomPoint = objRandom.Next(0, lstMutated.Count);
            var tempNumber = lstMutated[randomPoint];
            lstMutated.RemoveAt(randomPoint);
            var insertAt = objRandom.Next(0, lstMutated.Count);
            lstMutated.Insert(insertAt, tempNumber);

            return lstMutated;
        }
        public List<Coordinate> MutateIVM(List<Coordinate> vector)
        {
            if (objRandom.NextDouble() > Params.MutationRate)
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
        public List<Coordinate> MutateDIVM(List<Coordinate> vector)
        {
            var lstMutated = JJFunc.Copy(vector);
            if (objRandom.NextDouble() > Params.MutationRate) return lstMutated;

            int beg, end;
            beg = end = 0;
            JJFunc.ChooseSection(out beg, out end, lstMutated.Count, 2);
            var lstTemp = new List<Coordinate>();
            for (int i = beg; i < end; i++)
            {
                lstTemp.Add(lstMutated[beg]);
                lstMutated.RemoveAt(beg);
            }

            var numberOfSwaprsRequired = lstTemp.Count;
            while (numberOfSwaprsRequired != 0)
            {
                var no1 = objRandom.Next(0, lstTemp.Count);
                var no2 = objRandom.Next(0, lstTemp.Count);

                var temp = lstTemp[no1];
                lstTemp[no1] = lstTemp[no2];
                lstTemp[no2] = temp;

                --numberOfSwaprsRequired;
            }

            var insertLocation = objRandom.Next(0, lstMutated.Count + 1);
            var count = 0;
            for (int i = insertLocation; count < lstTemp.Count; i++)
            {
                lstMutated.Insert(i, lstTemp[count]);
                count++;
            }

            return lstMutated;
        }
        private void Crossover(List<Coordinate> mum, List<Coordinate> dad, out List<Coordinate> baby1, out List<Coordinate> baby2)
        {
            baby1 = JJFunc.Copy(mum);
            baby2 = JJFunc.Copy(dad);
            if (objRandom.NextDouble() > Params.CrossoverRate || JJFunc.AreEqual(mum, dad))
            {
                return;
            }

            var beg = objRandom.Next(0, mum.Count - 1);

            var end = beg;

            while (end <= beg)
            {
                end = objRandom.Next(0, mum.Count);
            }

            for (int pos = beg; pos < end + 1; ++pos)
            {
                var gene1 = mum[pos];
                var gene2 = dad[pos];

                if (gene1 != gene2)
                {
                    var posGene1 = baby1.IndexOf(gene1);
                    var posGene2 = baby1.IndexOf(gene2);

                    var temp = baby1[posGene1];
                    baby1[posGene1] = baby1[posGene2];
                    baby1[posGene2] = temp;

                    posGene1 = baby2.IndexOf(gene1);
                    posGene2 = baby2.IndexOf(gene2);

                    temp = baby2[posGene1];
                    baby2[posGene1] = baby2[posGene2];
                    baby2[posGene2] = temp;
                }
            }
        }
        private void CrossoverOBX(List<Coordinate> mum, List<Coordinate> dad, out List<Coordinate> baby1, out List<Coordinate> baby2)
        {
            baby1 = JJFunc.Copy(mum);
            baby2 = JJFunc.Copy(dad);
            if (objRandom.NextDouble() > Params.CrossoverRate || JJFunc.AreEqual(mum, dad))
            {
                return;
            }

            var lstTempCities = new List<Coordinate>();
            var lstPositions = new List<Coordinate>();

            var pos = objRandom.Next(0, mum.Count - 1);

            while (pos < mum.Count)
            {
                lstPositions.Add(new Coordinate(pos,0));
                lstTempCities.Add(mum[pos]);
                pos += objRandom.Next(1, mum.Count - pos);
            }

            var cPos = 0;
            for (int cit = 0; cit < baby2.Count; ++cit)
            {
                for (int i = 0; i < lstTempCities.Count; ++i)
                {
                    if (baby2[cit] == lstTempCities[i])
                    {
                        baby2[cit] = lstTempCities[cPos];
                        ++cPos;
                        break;
                    }
                }
            }

            lstTempCities.Clear();
            cPos = 0;

            for (int i = 0; i < lstPositions.Count - 1; ++i)
            {
                var x = Convert.ToInt32(lstPositions[i].X);
                lstTempCities.Add(dad[x]);
            }

            for (int cit = 0; cit < baby1.Count; ++cit)
            {
                for (int i = 0; i < lstTempCities.Count; ++i)
                {
                    if (baby1[cit] == lstTempCities[i])
                    {
                        baby1[cit] = lstTempCities[cPos];
                        ++cPos;
                        break;
                    }
                }
            }
        }
        private void CrossoverPBX(List<Coordinate> mum, List<Coordinate> dad, out List<Coordinate> baby1, out List<Coordinate> baby2)
        {
            baby1 = JJFunc.Copy(mum);
            baby2 = JJFunc.Copy(dad);

            if (objRandom.NextDouble() > Params.CrossoverRate || JJFunc.AreEqual(mum, dad))
                return;

            for (int i = 0; i < mum.Count; i++)
                baby1[i] = baby2[i] = new Coordinate(-1, 0);

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
                    baby2[c2] = mum[pos];

                while ((c1 < mum.Count) && (baby1[c1].X > -1))
                    ++c1;

                if (!baby1.Contains(dad[pos]))
                    baby1[c1] = dad[pos];
            }
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
                var tourLength = objMap.GetTourLength(lstPopulation[i].Route.ToList());
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
            return lstPopulation[fittestGenome].Route.ToList();
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
                lstNewPop.Add(lstPopulation[i]);

            while (lstNewPop.Count <= Params.PopulationSize)
            {
                var mom = RouletteWheelSelection();
                var dad = RouletteWheelSelection();

                var baby1List = new List<Coordinate>();
                var baby2List = new List<Coordinate>();

                CrossoverPBX(mom.Route.ToList(), dad.Route.ToList(), out baby1List, out baby2List);

                baby1List = MutateIVM(baby1List);
                baby2List = MutateIVM(baby2List);

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
        private void CreateStartingPopulation()
        {
            for (int i = 0; i < Params.PopulationSize; i++)
            {
                var objGenome = new GAGenome(Params.NumberOfRoutes);
                lstPopulation.Add(objGenome);
            }
        }
        private void Reset()
        {
            fittestGenome = 0;
            shortestRoute = double.MaxValue;
            longestRoute = 0;
        }
        
        
        private static List<GAGenome> Copy(List<GAGenome> listToCopy)
        {
            var lstReturn = new List<GAGenome>();
            for (int i = 0; i < listToCopy.Count; i++)
                lstReturn.Add(listToCopy[i]);

            return lstReturn;
        }
    }
}
