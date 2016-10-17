using Pathfinder.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pathfinder.Crossover
{
    public class CrossoverOBX : AbstractCrossover
    {
        public CrossoverOBX(double rate) : base(rate)
        {

        }
        public override CrossoverOperation Calc(CrossoverOperation Operation)
        {
            if (Settings.Random.NextDouble() > CrossoverRate || Operation.IsEqual())
            {
                return Operation;
            }
            var babymom = Operation.Copy(Operation.Mom);
            var babydad = Operation.Copy(Operation.Dad);

            var listmom = Operation.Mom.ListNodes;
            var listdad = Operation.Dad.ListNodes;

            var listbabymom = babymom.ListNodes;
            var listbabydad = babydad.ListNodes;
            var minindex = Math.Min(listmom.Count, listdad.Count);

            var beg = Settings.Random.Next(0, minindex - 1);

            var end = beg;

            while (end < beg)
                end = Settings.Random.Next(0, minindex);

            for (int pos = beg; pos <= end; ++pos)
            {
                var genemom = listmom[pos];
                var genedad = listdad[pos];

                if (!genemom.Equals(genedad))
                {
                    var temp = listbabymom[pos];
                    listbabymom[pos] = listdad[pos];
                    listdad[pos] = temp;
                }
            }

            return new CrossoverOperation(babymom, babydad);
        }
    }
}
