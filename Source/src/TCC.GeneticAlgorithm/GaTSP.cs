using System;
using System.Collections.Generic;
using System.Linq;
using TCC.Core;

namespace TCC.GeneticAlgorithm
{
    public class GATSP
    {
        private List<GAGenome> lstPopulation;
        private GAMapTSP objMap;

        private GAParams Params;
        private int NumBest2Add = 1;

        private double totalFitness;
        private double shortestRoute;
        public double BestSolution
        {
            get { return shortestRoute; }
        }
        private double longestRoute;
        private int fittestGenome;
        public int generation;
        private Random objRandom;

        public GATSP(GAParams tParams)
        {
            lstPopulation = new List<GAGenome>();

            Params = tParams;

            fittestGenome = 0;
            generation = 0;
            shortestRoute = double.MaxValue;
            longestRoute = 0;

            objRandom = new Random();

            objMap = new GAMapTSP(tParams.numberOfCities, tParams.MapaSize);
            CreateStartingPopulation();
        }

        public List<Coordinate> GetlstCityCoordinates()
        {
            return objMap.lstCityCoordinates;
        }
        private List<int> Mutation(List<int> vector)
        {
            var lstMutated = Copy(vector);

            if (objRandom.NextDouble() > Params.mutationRate) return lstMutated;

            var pos1 = objRandom.Next(0, Params.numberOfCities);
            var pos2 = pos1;

            while (pos1 == pos2)
            {
                pos2 = objRandom.Next(0, Params.numberOfCities);
            }

            var temp = lstMutated[pos1];
            lstMutated[pos1] = lstMutated[pos2];
            lstMutated[pos2] = temp;

            return lstMutated;
        }
        public List<int> MutateSM(List<int> vector)
        {
            var lstMutated = Copy(vector);
            if (objRandom.NextDouble() > Params.mutationRate) return lstMutated;

            const int minSpanSize = 3;
            int beg, end;
            beg = end = 0;
            ChooseSection(out beg, out end, lstMutated.Count, minSpanSize);

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
        private void ChooseSection(out int beg, out int end, int maxSpanSize, int minSpanSize)
        {
            var spanSize = objRandom.Next(minSpanSize, maxSpanSize);
            beg = objRandom.Next(0, maxSpanSize - spanSize);
            end = beg + spanSize;
        }
        public List<int> MutateDM(List<int> vector)
        {
            var lstMutated = Copy(vector);
            if (objRandom.NextDouble() > Params.mutationRate) return lstMutated;

            const int minSpanSize = 3;
            int beg, end;
            beg = end = 0;
            ChooseSection(out beg, out end, lstMutated.Count, minSpanSize);

            var lstTemp = new List<int>();
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
        public List<int> MutateIM(List<int> vector)
        {
            var lstMutated = Copy(vector);
            if (objRandom.NextDouble() > Params.mutationRate) return lstMutated;

            var randomPoint = objRandom.Next(0, lstMutated.Count);
            var tempNumber = lstMutated[randomPoint];
            lstMutated.RemoveAt(randomPoint);
            var insertAt = objRandom.Next(0, lstMutated.Count);
            lstMutated.Insert(insertAt, tempNumber);

            return lstMutated;
        }
        public List<int> MutateIVM(List<int> vector)
        {
            if (objRandom.NextDouble() > Params.mutationRate)
                return vector;

            var lstMutated = Copy(vector);
            int beg, end;
            beg = end = 0;

            ChooseSection(out beg, out end, lstMutated.Count, 2);
            var lstTemp = new List<int>();
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
        public List<int> MutateDIVM(List<int> vector)
        {
            var lstMutated = Copy(vector);
            if (objRandom.NextDouble() > Params.mutationRate) return lstMutated;

            int beg, end;
            beg = end = 0;
            ChooseSection(out beg, out end, lstMutated.Count, 2);
            var lstTemp = new List<int>();
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


        private void Crossover(List<int> mum, List<int> dad, out List<int> baby1, out List<int> baby2)
        {
            baby1 = Copy(mum);
            baby2 = Copy(dad);
            if (objRandom.NextDouble() > Params.crossoverRate || AreEqual(mum, dad))
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
        private void CrossoverOBX(List<int> mum, List<int> dad, out List<int> baby1, out List<int> baby2)
        {
            baby1 = Copy(mum);
            baby2 = Copy(dad);
            if (objRandom.NextDouble() > Params.crossoverRate || AreEqual(mum, dad))
            {
                return;
            }

            var lstTempCities = new List<int>();
            var lstPositions = new List<int>();

            var pos = objRandom.Next(0, mum.Count - 1);

            while (pos < mum.Count)
            {
                lstPositions.Add(pos);
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
                lstTempCities.Add(dad[lstPositions[i]]);
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
        private void CrossoverPBX(List<int> mum, List<int> dad, out List<int> baby1, out List<int> baby2)
        {
            baby1 = Copy(mum);
            baby2 = Copy(dad);

            if (objRandom.NextDouble() > Params.crossoverRate || AreEqual(mum, dad))
                return;

            for (int i = 0; i < mum.Count; i++)
                baby1[i] = baby2[i] = -1;

            //var l = baby2.Count;

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
                while (c2 < mum.Count && baby2[c2] > -1)
                    ++c2;
                if (!baby2.Contains(mum[pos]))
                    baby2[c2] = mum[pos];

                while ((c1 < mum.Count) && (baby1[c1] > -1))
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

            for (int i = 0; i < Params.populationSize; i++)
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
            for (int i = 0; i < Params.populationSize; ++i)
            {
                var tourLength = objMap.GetTourLength(lstPopulation[i].Cities);
                lstPopulation[i].Fitness = tourLength;

                if (tourLength < shortestRoute)
                {
                    shortestRoute = tourLength;
                    fittestGenome = i;
                }
                if (tourLength > longestRoute)
                    longestRoute = tourLength;
            }

            for (int i = 0; i < Params.populationSize; ++i)
            {
                lstPopulation[i].Fitness = longestRoute - lstPopulation[i].Fitness;
                totalFitness += lstPopulation[i].Fitness;
            }
        }

        public List<int> GetBestCitie()
        {
            return lstPopulation[fittestGenome].Cities;
        }
        public List<Coordinate> GetCityCoordinates()
        {
            return objMap.lstCityCoordinates;
        }

        public void Epoch()
        {
            Reset();

            CalculatePopulationFitness();

            var lstNewPop = new List<GAGenome>();

            lstPopulation = lstPopulation.OrderByDescending(x => x.Fitness).ToList();
            for (int i = 0; i < NumBest2Add; i++)
                lstNewPop.Add(lstPopulation[i]);

            while (lstNewPop.Count <= Params.populationSize)
            {
                var mom = RouletteWheelSelection();
                var dad = RouletteWheelSelection();

                var baby1List = new List<int>();
                var baby2List = new List<int>();

                CrossoverPBX(mom.Cities, dad.Cities, out baby1List, out baby2List);

                baby1List = MutateIVM(baby1List);
                baby2List = MutateIVM(baby2List);

                GAGenome baby1, baby2;
                baby1 = new GAGenome();
                baby2 = new GAGenome();

                baby1.Cities = baby1List;
                baby2.Cities = baby2List;

                lstNewPop.Add(baby1);
                lstNewPop.Add(baby2);
            }
            lstPopulation = Copy(lstNewPop);

            ++generation;
        }

        private void CreateStartingPopulation()
        {
            for (int i = 0; i < Params.populationSize; i++)
            {
                var objGenome = new GAGenome(Params.numberOfCities, objRandom);
                lstPopulation.Add(objGenome);
            }
        }

        private void Reset()
        {
            fittestGenome = 0;
            shortestRoute = double.MaxValue;
            longestRoute = 0;
        }

        private static bool AreEqual(List<int> lst1, List<int> lst2)
        {
            if (lst1.Count != lst2.Count)
                return false;

            for (int i = 0; i < lst1.Count; i++)
                if (lst1[i] != lst2[i])
                    return false;

            return true;
        }

        private static List<int> Copy(List<int> listToCopy)
        {
            var lstReturn = new List<int>();
            for (int i = 0; i < listToCopy.Count; i++)
                lstReturn.Add(listToCopy[i]);

            return lstReturn;
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
