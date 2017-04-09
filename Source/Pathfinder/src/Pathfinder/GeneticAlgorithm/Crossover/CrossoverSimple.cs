using Pathfinder.Abstraction;
using System;
namespace Pathfinder.Crossover
{
    public class CrossoverSimple : AbstractCrossover
    {
        public override CrossoverOperation Calc(CrossoverOperation Operation)
        {
            var rand = Container.Resolve<IRandom>();
            if (rand.NextDouble() > CrossoverRate || Operation.IsEqual())
                return Operation;
            var babymom = CrossoverOperation.Copy(Operation.Mom);
            var babydad = CrossoverOperation.Copy(Operation.Dad);
            var listmom = Operation.Mom.ListNodes;
            var listdad = Operation.Dad.ListNodes;
            var listbabymom = babymom.ListNodes;
            var listbabydad = babydad.ListNodes;
            var minindex = Math.Min(listmom.Count, listdad.Count);
            var beg = rand.Next(0, minindex - 1);
            var end = beg;
            while (end < beg)
                end = rand.Next(0, minindex);
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