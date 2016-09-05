using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TCC.GeneticAlgorithm
{
    public static class GA
    {
        public static List<GAGenome> CopyGenome(List<GAGenome> listToCopy)
        {
            var lstReturn = new List<GAGenome>();
            for (int i = 0; i < listToCopy.Count; i++)
                lstReturn.Add(listToCopy[i]);

            return lstReturn;
        }
    }
}
