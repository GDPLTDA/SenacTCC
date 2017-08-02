using Pathfinder.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Pathfinder.Mutation
{
    public class MutateSM : AbstractMutate
    {
        public override IGenome Calc(IGenome baby)
        {
            var rand = Container.Resolve<IRandom>();
            if (rand.NextDouble() > MutationRate || baby.ListNodes.Count < 3)
                return baby;
            var listcount = baby.ListNodes.Count;
            const int minSpanSize = 3;
            if (listcount <= minSpanSize)
                return baby;
            int beg, end;
            beg = end = 0;
            var spanSize = rand.Next(minSpanSize, listcount);
            beg = rand.Next(1, listcount - spanSize);
            end = beg + spanSize;
            var span = end - beg;
            var numberOfSwaprsRequired = span;
            while (numberOfSwaprsRequired != 0)
            {
                var no1 = rand.Next(beg, end);
                var no2 = rand.Next(beg, end);
                var temp = baby.ListNodes[no1];
                baby.ListNodes[no1] = baby.ListNodes[no2];
                baby.ListNodes[no2] = temp;
                --numberOfSwaprsRequired;
            }
            return baby;
        }
    }
}