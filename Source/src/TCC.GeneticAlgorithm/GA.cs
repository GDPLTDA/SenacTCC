using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TCC.GeneticAlgorithm
{
    public static class GA
    {
        public static List<GAGenome> CopyGenome(List<GAGenome> listToCopy,Random ObjRandom)
        {
            var lstReturn = new List<GAGenome>();

            foreach (var item in listToCopy)
            {
                lstReturn.Add(new GAGenome(item.Route, ObjRandom));
            }              

            return lstReturn;
        }
    }
}
