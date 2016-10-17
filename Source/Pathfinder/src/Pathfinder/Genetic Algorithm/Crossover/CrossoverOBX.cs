using Pathfinder.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pathfinder.Crossover
{
    public class CrossoverOBX : AbstractCrossover
    {
        public override CrossoverOperation Calc(CrossoverOperation Operation)
        {
            if (Settings.Random.NextDouble() > CrossoverRate || Operation.IsEqual())
            {
                return Operation;
            }
            var babymom = Operation.Copy(Operation.Mom);
            var babydad = Operation.Copy(Operation.Dad);

            var listmom = babymom.ListNodes;
            var listdad = babydad.ListNodes;

            var beg = Settings.Random.Next(0, listmom.Count - 1);

            var end = beg;

            while (end <= beg)
                end = Settings.Random.Next(0, listmom.Count);

            //for (int pos = beg; pos < end + 1; ++pos)
            //{
            //    var gene1 = mom[pos];
            //    var gene2 = dad[pos];

            //    if (gene1 != gene2)
            //    {
            //        var posGene1 = baby1.IndexOf(gene1);
            //        var posGene2 = baby1.IndexOf(gene2);

            //        var temp = baby1[posGene1];
            //        baby1[posGene1] = baby1[posGene2];
            //        baby1[posGene2] = temp;

            //        posGene1 = baby2.IndexOf(gene1);
            //        posGene2 = baby2.IndexOf(gene2);

            //        temp = baby2[posGene1];
            //        baby2[posGene1] = baby2[posGene2];
            //        baby2[posGene2] = temp;
            //    }
            //}

            return new CrossoverOperation(babymom, babydad);
        }
    }
}
