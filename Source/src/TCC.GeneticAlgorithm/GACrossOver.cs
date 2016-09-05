using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCC.Core;

namespace TCC.GeneticAlgorithm
{
    public class GACrossOver
    {
        private GAParams Params { get; set; }
        private Random objRandom { get; set; }
        public GACrossOver(GAParams tParams, Random tRan)
        {
            Params = tParams;
            objRandom = tRan;
        }

        public void Crossover(List<Coordinate> mum, List<Coordinate> dad, out List<Coordinate> baby1, out List<Coordinate> baby2)
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
        public void CrossoverOBX(List<Coordinate> mum, List<Coordinate> dad, out List<Coordinate> baby1, out List<Coordinate> baby2)
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
                lstPositions.Add(new Coordinate(pos, 0));
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
        public void CrossoverPBX(List<Coordinate> mum, List<Coordinate> dad, out List<Coordinate> baby1, out List<Coordinate> baby2)
        {
            mum = JJFunc.EqualSize(mum, dad);
            dad = JJFunc.EqualSize(dad, mum);

            baby1 = JJFunc.Copy(mum);
            baby2 = JJFunc.Copy(dad);

            if (objRandom.NextDouble() > Params.CrossoverRate || JJFunc.AreEqual(mum, dad))
                return;

            for (int i = 0; i < mum.Count; i++)
                baby1[i] = baby2[i] = new Coordinate(-1, -1);

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

            for (int pos = 0; pos < mum.Count; pos++)
            {
                while (c2 < mum.Count && baby2[c2].Xi > -1)
                    ++c2;
                if (c2 < baby2.Count && !baby2.Exists(i => i.Xi == mum[pos].Xi && i.Yi == mum[pos].Yi))
                    baby2[c2] = mum[pos];

                while (c1 < mum.Count && baby1[c1].Xi > -1)
                    ++c1;

                if (c1 < baby1.Count && !baby1.Exists(i=> i.Xi ==  dad[pos].Xi &&  i.Yi == dad[pos].Yi))
                    baby1[c1] = dad[pos];
            }
        }
    }
}
