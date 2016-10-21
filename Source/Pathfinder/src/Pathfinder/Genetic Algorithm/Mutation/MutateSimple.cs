using Pathfinder.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pathfinder.Mutation
{ 
    public class MutateSimple : AbstractMutate
    {
        public override IGenome Calc(IGenome baby)
        {
            if (Settings.Random.NextDouble() > MutationRate || baby.ListNodes.Count < 3)
                return baby;

            int listcount = baby.ListNodes.Count;
            // Ignora o inicial
            var pos1 = Settings.Random.Next(1, listcount);
            var pos2 = pos1;

            while (pos1 == pos2)
                pos2 = Settings.Random.Next(1, listcount); // Ignora o inicial

            var temp = baby.ListNodes[pos1];
            baby.ListNodes[pos1] = baby.ListNodes[pos2];
            baby.ListNodes[pos2] = temp;

            return baby;
        }
    }
}
