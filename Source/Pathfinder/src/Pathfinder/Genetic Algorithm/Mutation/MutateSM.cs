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
            if (Settings.Random.NextDouble() > MutationRate || baby.ListNodes.Count < 3)
                return baby;

            int listcount = baby.ListNodes.Count;
            const int minSpanSize = 3;

            if (listcount <= minSpanSize)
                return baby;

            int beg, end;
            beg = end = 0;

            var spanSize = Settings.Random.Next(minSpanSize, listcount);
            beg = Settings.Random.Next(1, listcount - spanSize);
            end = beg + spanSize;

            var span = end - beg;
            var numberOfSwaprsRequired = span;

            while (numberOfSwaprsRequired != 0)
            {
                var no1 = Settings.Random.Next(beg, end);
                var no2 = Settings.Random.Next(beg, end);

                var temp = baby.ListNodes[no1];
                baby.ListNodes[no1] = baby.ListNodes[no2];
                baby.ListNodes[no2] = temp;

                --numberOfSwaprsRequired;
            }

            return baby;
        }
    }
}
