using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCC.Core;

namespace TCC.GeneticAlgorithm
{
    public class GAMutate
    {
        private GAParams Params { get; set; }
        private Random objRandom { get; set; }
        public GAMutate(GAParams tParams, Random tRan)
        {
            Params = tParams;
            objRandom = tRan;
        }

        public List<Coordinate> Mutation(List<Coordinate> vector)
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
    }
}
