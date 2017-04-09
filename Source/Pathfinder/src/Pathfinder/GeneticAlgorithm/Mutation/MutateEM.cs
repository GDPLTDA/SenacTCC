using Pathfinder.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Pathfinder.Mutation
{
    public class MutateEM : AbstractMutate
    {
        public override IGenome Calc(IGenome baby)
        {
            var rand = Container.Resolve<IRandom>();
            if (rand.NextDouble() > MutationRate || baby.ListNodes.Count < 3)
                return baby;
            var listcount = baby.ListNodes.Count;
            // Ignora o inicial
            var pos1 = rand.Next(0, listcount);
            var pos2 = pos1;
            while (pos1 == pos2)
                pos2 = rand.Next(0, listcount); // Ignora o inicial
            var temp = baby.ListNodes[pos1];
            baby.ListNodes[pos1] = baby.ListNodes[pos2];
            baby.ListNodes[pos2] = temp;
            return baby;
        }
    }
}