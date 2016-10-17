using Pathfinder.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pathfinder.Mutation
{
    public class MutateIVM : IMutate
    {
        public double MutationRate { get; set; }
        public MutateIVM(double rate)
        {
            MutationRate = rate;
        }
        public IGenome Calc(IGenome baby)
        {
            return baby;
        }
    }
}
